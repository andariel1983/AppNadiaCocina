namespace AppAndroidHolaMundo.Presentation;

// Logger centralizado con un tag fijo, para poder filtrar en logcat con:
//   adb logcat -s AppAndroidHolaMundo:V
// Usa Android.Util.Log directamente (además de Debug.WriteLine) porque es
// lo único que garantiza aparecer en logcat incluso en builds sin depurador
// adjunto, que es el escenario real de trabajo (celular físico por Wi-Fi).
//
// El proyecto Presentation hoy compila solo para net10.0-android (ver
// TargetFrameworks en el .csproj), así que se llama a Android.Util.Log
// directo, sin "#if ANDROID": esa constante no se está definiendo en este
// build y el guard silenciaba el logging sin avisar. Si en el futuro se
// agrega otro target (iOS/Windows), hay que reintroducir el guard.
public static class Logger
{
    private const string Tag = "AppAndroidHolaMundo";

    public static void Info(string mensaje)
    {
        Android.Util.Log.Info(Tag, mensaje);
        System.Diagnostics.Debug.WriteLine($"[{Tag}] INFO: {mensaje}");
    }

    public static void Error(string contexto, Exception exception)
    {
        var mensaje = $"{contexto} -> {exception}";
        Android.Util.Log.Error(Tag, mensaje);
        System.Diagnostics.Debug.WriteLine($"[{Tag}] ERROR: {mensaje}");
    }
}
