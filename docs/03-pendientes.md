# Pendientes

## Hecho

- `emulator` y `system-images;android-35;google_apis;x86_64` instalados.
- AVD creado: **`Pixel5_API35`** (Pixel 5, API 35, google_apis, x86_64),
  en `~/.android/avd/Pixel5_API35.avd`.
- Probado en modo headless (`-no-window`): **bootea completo y usa
  aceleración KVM sin errores** (confirmado con `adb shell getprop
  sys.boot_completed` = `1`). Se apagó después de la prueba
  (`adb emu kill`) — no quedó corriendo.

- **Probado en dispositivo físico real** (Samsung Galaxy S23 Ultra, Android
  16, conectado por Wi-Fi con `adb pair`/`adb connect`): la app instala,
  abre y muestra "¡Hola Mundo desde Android!" correctamente. Confirmado
  por el usuario.

## Bug encontrado y resuelto: el APK abría y se cerraba solo

**Causa:** el build Debug usa *Fast Deployment* de .NET Android — no
empaqueta los ensamblados .NET dentro del APK, asume que el IDE los copia
aparte al instalar. Instalando el APK a mano (`adb install`) quedaba sin
código y abortaba al toque (`monodroid: No assemblies found...`,
`SIGABRT`).

**Fix:** en `CocinaNadia.Presentation.csproj` se agregó
`EmbedAssembliesIntoApk=true` también para Debug, para que el APK quede
autocontenido.

## Mejora agregada: manejo global de errores

A pedido del usuario, un error no controlado ya no cierra la app: se
muestra en pantalla el texto completo de la excepción (seleccionable +
botón "Copiar al portapapeles"). Ver `ManejadorErroresGlobal.cs`,
`ErrorPage.cs` y el hook `AndroidEnvironment.UnhandledExceptionRaiser` en
`Platforms/Android/MainApplication.cs`.

## Falta (opcional, no bloqueante)

1. **Probar debug real desde VS Code** (F5 con la configuración "Depurar en
   Android (MAUI)"): poner un breakpoint en `ObtenerSaludoUseCase.Ejecutar()`
   o en `MainPage.xaml.cs` y confirmar que corta ahí. Nunca se probó
   directamente en VS Code — todo lo verificado hasta ahora fue por consola.
2. El emulador (`Pixel5_API35`) quedó creado y probado (bootea con KVM),
   pero **no se usó para el flujo final** — se terminó validando contra el
   celular físico del usuario, que resultó más directo.

## Decisiones que quedaron sin confirmar con el usuario

- Nombre y specs del AVD (API level, tamaño de pantalla, RAM) — se va a usar
  un dispositivo genérico razonable (ej. Pixel 5, API 35) salvo que se pida
  otra cosa.
- No se probó todavía compilar en modo `Release` ni generar un APK firmado
  para instalar fuera del emulador — fuera del alcance original ("Hola
  Mundo" + capas), no se hizo porque no se pidió.

## Riesgo conocido

- El grupo `kvm` fue agregado con `sudo usermod -aG kvm rutherford` y
  confirmado por el usuario. Para que **todas** las sesiones nuevas
  (incluida la de VS Code) tengan el grupo aplicado, hace falta cerrar
  sesión gráfica por completo y volver a entrar — un `newgrp kvm` en una
  terminal puntual no alcanza para procesos lanzados desde otro lado (ej.
  VS Code abierto antes del cambio).
