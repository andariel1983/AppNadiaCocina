# Entorno instalado en esta máquina

Todo se instaló en el home del usuario (`~/android-tools/`), sin tocar
rutas del sistema salvo lo indicado explícitamente.

## Vía `dotnet workload install` (SDK de .NET)

- Workload **`maui-android`** (incluye Microsoft.Maui.Sdk, templates, runtimes
  Android arm/arm64/x86/x64).

## Descargado manualmente (sin sudo)

| Componente | Ubicación | Origen |
|---|---|---|
| JDK 17 (Temurin) | `~/android-tools/jdk17/` | api.adoptium.net |
| Android cmdline-tools | `~/android-tools/android-sdk/cmdline-tools/latest/` | dl.google.com |
| Android platform-tools | `~/android-tools/android-sdk/platform-tools/` | instalado vía `sdkmanager` |
| Android platform 35 | `~/android-tools/android-sdk/platforms/android-35/` | instalado vía `sdkmanager` |
| Android platform 36 | `~/android-tools/android-sdk/platforms/android-36/` | instalado vía `sdkmanager` (requerido por el SDK Android 36.1.2 que usa MAUI) |
| Android build-tools 35.0.0 | `~/android-tools/android-sdk/build-tools/35.0.0/` | instalado vía `sdkmanager` |
| Emulador Android | `~/android-tools/android-sdk/emulator/` | **en progreso**, ver pendientes |
| System image `android-35;google_apis;x86_64` | `~/android-tools/android-sdk/system-images/` | **en progreso**, ver pendientes |

`Directory.Build.props` en la raíz del proyecto fija `AndroidSdkDirectory` y
`JavaSdkDirectory` a estas rutas, para que `dotnet build` funcione sin
exportar variables de entorno.

## Con `sudo` (único paso que requirió permisos de sistema)

- Se agregó el usuario `rutherford` al grupo `kvm` (`agregar_usuario_grupo_kvm.sh`
  en `/home/rutherford/`), necesario para que el emulador Android acelere por
  hardware (KVM). **Ya ejecutado y confirmado.** Para que aplique en todas las
  terminales nuevas hace falta cerrar sesión gráfica y volver a entrar (un
  `newgrp kvm` puntual sirve solo para la terminal donde se corre).

## VS Code

Extensiones instaladas:
- `ms-dotnettools.csdevkit` (C# Dev Kit)
- `ms-dotnettools.dotnet-maui`

Configuración en `.vscode/` (dentro del proyecto):
- `settings.json`: apunta la extensión MAUI a `androidSdkPath`/`javaSdkPath`.
- `tasks.json`: tarea `build-android` (`dotnet build ... -t:Run`).
- `launch.json`: configuración "Depurar en Android (MAUI)" para F5.
