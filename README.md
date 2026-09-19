# CocinaNadia

App Android "Hola Mundo" en .NET MAUI, con Clean Architecture (4 capas en
proyectos separados).

## Estructura

```
CocinaNadia.Domain/          # Entidades de negocio. No depende de nada.
CocinaNadia.Application/     # Casos de uso + puertos (interfaces).
CocinaNadia.Infrastructure/  # Implementaciones concretas de los puertos.
CocinaNadia.Presentation/    # App .NET MAUI (UI), target: net10.0-android.
```

Flujo de dependencias: `Presentation` → `Application` + `Infrastructure` →
`Domain`. El dominio no conoce a nadie; la UI solo conoce casos de uso
(`ObtenerSaludoUseCase`), nunca la implementación concreta — eso lo resuelve
la inyección de dependencias configurada en `MauiProgram.cs`.

## Compilar

```bash
dotnet build CocinaNadia.Presentation/CocinaNadia.Presentation.csproj
```

No hace falta exportar `JAVA_HOME` ni `ANDROID_SDK_ROOT`: `Directory.Build.props`
ya apunta al JDK y Android SDK instalados en `~/android-tools/` (sin sudo,
fuera de rutas del sistema).

## Correr en un dispositivo/emulador Android

```bash
export PATH="/home/rutherford/android-tools/jdk17/bin:/home/rutherford/android-tools/android-sdk/platform-tools:$PATH"
adb devices   # verificar que el dispositivo/emulador esté conectado
dotnet build CocinaNadia.Presentation/CocinaNadia.Presentation.csproj -t:Run
```

## Entorno instalado

- JDK 17 (Temurin): `~/android-tools/jdk17`
- Android SDK (platform-tools, platforms 35/36, build-tools 35.0.0): `~/android-tools/android-sdk`
- Workload dotnet: `maui-android`
