using Microsoft.Extensions.Logging;

namespace AppAndroidHolaMundo.Presentation;

// Conecta Microsoft.Extensions.Logging (usado en Application/Infrastructure)
// con Android.Util.Log, todo bajo el mismo tag fijo que usa Logger.cs.
// Así "adb logcat -s AppAndroidHolaMundo:*" muestra los logs de TODAS las
// capas (UI, casos de uso, infraestructura), no solo los de Presentation.
public sealed class AndroidLogLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new AndroidLogLogger(categoryName);

    public void Dispose() { }

    private sealed class AndroidLogLogger(string categoryName) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var mensaje = $"[{categoryName}] {formatter(state, exception)}";

            if (exception is not null)
                Logger.Error(mensaje, exception);
            else
                Logger.Info(mensaje);
        }
    }
}
