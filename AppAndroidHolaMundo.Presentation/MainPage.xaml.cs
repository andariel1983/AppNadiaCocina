using AppAndroidHolaMundo.Application;

namespace AppAndroidHolaMundo.Presentation;

public partial class MainPage : ContentPage
{
    // La UI depende únicamente del caso de uso (capa Application), nunca de
    // Infrastructure directamente. Quién resuelve la implementación real es
    // el contenedor de DI configurado en MauiProgram.cs.
    private readonly ObtenerSaludoUseCase _obtenerSaludoUseCase;

    public MainPage(ObtenerSaludoUseCase obtenerSaludoUseCase)
    {
        InitializeComponent();
        _obtenerSaludoUseCase = obtenerSaludoUseCase;

        try
        {
            var saludo = _obtenerSaludoUseCase.Ejecutar();
            SaludoLabel.Text = saludo.Mensaje;
            Logger.Info($"MainPage cargada. Saludo mostrado: \"{saludo.Mensaje}\"");
        }
        catch (Exception exception)
        {
            // Se loguea con contexto propio (en qué pantalla y qué caso de uso)
            // y se relanza: el manejador global (ManejadorErroresGlobal) es
            // el que decide qué mostrar en pantalla, este catch es solo para
            // dejar registro de en qué punto exacto se originó.
            Logger.Error($"{nameof(MainPage)}: falló {nameof(_obtenerSaludoUseCase.Ejecutar)}", exception);
            throw;
        }
    }
}
