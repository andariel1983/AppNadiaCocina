namespace CocinaNadia.Presentation;

// Punto único donde se decide qué hacer cuando algo no controlado explota:
// en vez de dejar que la app se cierre, se muestra el error en pantalla
// (texto seleccionable/copiable) para poder diagnosticarlo sin logcat a mano.
//
// Se usa "Microsoft.Maui.Controls.Application" con el namespace completo
// porque el proyecto también tiene una capa "CocinaNadia.Application"
// (Clean Architecture) que, al compartir el namespace raíz, tapa a la clase
// de MAUI en la resolución de nombres.
public static class ManejadorErroresGlobal
{
    public static void Mostrar(Exception exception)
    {
        var texto = exception.ToString();
        Logger.Error("Excepción no controlada", exception);

        var aplicacion = Microsoft.Maui.Controls.Application.Current;
        if (aplicacion is null || aplicacion.Windows.Count == 0)
            return;

        var ventana = aplicacion.Windows[0];

        // Los manejadores de excepciones pueden dispararse en cualquier hilo;
        // la UI de MAUI solo se puede tocar desde el hilo principal.
        aplicacion.Dispatcher.Dispatch(() =>
        {
            ventana.Page = new ErrorPage(texto);
        });
    }
}
