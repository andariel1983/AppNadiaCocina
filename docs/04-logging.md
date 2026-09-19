# Logging

A pedido del usuario, todo el código loguea con detalle ante errores (y en
puntos clave del flujo normal), pensado para depurar por `adb logcat` ya
que no hay emulador ni debugger visual conectado — se trabaja contra el
celular físico del usuario por Wi-Fi.

## Cómo ver los logs

```bash
export PATH="/home/rutherford/android-tools/android-sdk/platform-tools:$PATH"
adb logcat -s AppAndroidHolaMundo:V
```

Todo lo que loguea cualquier capa (Domain no loguea porque no debe
depender de nada; Application e Infrastructure vía `ILogger<T>`;
Presentation vía `Logger`/excepciones globales) sale bajo el tag fijo
`AppAndroidHolaMundo`, sin importar de qué capa venga.

## Piezas

- **`Logger.cs`** (Presentation): logger simple con tag fijo, usa
  `Android.Util.Log` directo + `Debug.WriteLine`.
- **`AndroidLogLoggerProvider.cs`** (Presentation): conecta
  `Microsoft.Extensions.Logging` (`ILogger<T>`, usado en Application e
  Infrastructure) con `Logger.cs`, para que todo termine en el mismo tag.
- **`ManejadorErroresGlobal.cs`**: loguea con `Logger.Error` cualquier
  excepción no controlada, antes de mostrarla en `ErrorPage`.
- Los puntos de entrada de cada capa (`MainPage`, `ObtenerSaludoUseCase`,
  `ProveedorSaludoEstatico`) tienen try/catch propio: loguean con su
  contexto específico y relanzan (`throw;`) — el manejador global es el
  que decide qué hacer con la UI, estos catches son solo para dejar
  registro de en qué capa exacta se originó el problema.

## Bug encontrado y resuelto: `#if ANDROID` no funcionaba

La primera versión de `Logger.cs` envolvía las llamadas a
`Android.Util.Log` en `#if ANDROID ... #endif`. Se comprobó con:

```bash
dotnet build AppAndroidHolaMundo.Presentation.csproj -f net10.0-android -getProperty:DefineConstants
# devolvió: TRACE;DEBUG   (sin "ANDROID")
```

que esa constante **no está definida** en este SDK/build, así que el
bloque quedaba compilado afuera silenciosamente — no tiraba error, pero
tampoco logueaba nada a logcat. Como `Presentation` hoy solo tiene un
target (`net10.0-android`), se sacó el `#if ANDROID` y se llama a
`Android.Util.Log` directo. **Si en el futuro se agrega otro target**
(iOS/Windows), hay que reintroducir un guard — probablemente
`$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'android'`
en vez de confiar en la constante `ANDROID`.
