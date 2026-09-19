using Microsoft.Extensions.Logging;
using CocinaNadia.Application;
using CocinaNadia.Infrastructure;

namespace CocinaNadia.Presentation;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Registro de dependencias (Dependency Injection): acá es el único lugar
		// de todo el proyecto que sabe qué implementación concreta usar para cada puerto.
		builder.Services.AddSingleton<IProveedorSaludo, ProveedorSaludoEstatico>();
		builder.Services.AddTransient<ObtenerSaludoUseCase>();
		builder.Services.AddTransient<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif
		// Todo ILogger<T> inyectado en cualquier capa (Application, Infrastructure,
		// Presentation) termina acá, filtrable con: adb logcat -s CocinaNadia:*
		builder.Logging.AddProvider(new AndroidLogLoggerProvider());
		builder.Logging.SetMinimumLevel(LogLevel.Debug);

		// Errores no controlados en hilos que no son la UI (ej. código async
		// "fire and forget"). En Android, el crash que realmente mata el proceso
		// se maneja aparte, en Platforms/Android/MainApplication.cs.
		AppDomain.CurrentDomain.UnhandledException += (_, args) =>
		{
			if (args.ExceptionObject is Exception exception)
				ManejadorErroresGlobal.Mostrar(exception);
		};
		TaskScheduler.UnobservedTaskException += (_, args) =>
		{
			ManejadorErroresGlobal.Mostrar(args.Exception);
			args.SetObserved();
		};

		return builder.Build();
	}
}
