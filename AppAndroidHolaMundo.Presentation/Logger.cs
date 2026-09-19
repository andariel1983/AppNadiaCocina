namespace AppAndroidHolaMundo.Presentation;

// Logger centralizado con un tag fijo, para poder filtrar en logcat con:
//   adb logcat -s AppAndroidHolaMundo:*
// Usa Android.Util.Log directamente (además de Debug.WriteLine) porque es
// lo único que garantiza aparecer en logcat incluso en builds sin depurador
// adjunto, que es el escenario real de trabajo (celular físico por Wi-Fi).
public static class Logger
{
    private const string Tag = "AppAndroidHolaMundo";

    public static void Info(string mensaje)
    {
#if ANDROID
        Android.Util.Log.Info(Tag, mensaje);
#endif
        System.Diagnostics.Debug.WriteLine($"[{Tag}] INFO: {mensaje}");
    }

    public static void Error(string contexto, Exception exception)
    {
        var mensaje = $"{contexto} -> {exception}";
#if ANDROID
        Android.Util.Log.Error(Tag, mensaje);
#endif
        System.Diagnostics.Debug.WriteLine($"[{Tag}] ERROR: {mensaje}");
    }
}
