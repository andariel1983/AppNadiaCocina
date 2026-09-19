using Android.App;
using Android.Runtime;

namespace AppAndroidHolaMundo.Presentation;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
		// Este es el manejador que realmente importa en Android: cuando una
		// excepción managed cruza al lado nativo sin ser atrapada, el runtime
		// mata el proceso (SIGABRT) salvo que se marque Handled = true acá.
		AndroidEnvironment.UnhandledExceptionRaiser += (_, args) =>
		{
			args.Handled = true;
			ManejadorErroresGlobal.Mostrar(args.Exception);
		};
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
