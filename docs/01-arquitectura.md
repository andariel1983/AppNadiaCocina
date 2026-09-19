# Arquitectura

App Android "Hola Mundo" en .NET MAUI, con Clean Architecture (capas como
proyectos `.csproj` separados dentro de la misma solución).

## Proyectos y dependencias

```
CocinaNadia.Presentation  (net10.0-android, MAUI)
        │
        ├──> CocinaNadia.Application
        │           │
        │           └──> CocinaNadia.Domain
        │
        └──> CocinaNadia.Infrastructure
                    │
                    ├──> CocinaNadia.Domain
                    └──> CocinaNadia.Application
```

- **Domain** (`Saludo.cs`): entidad de negocio. No depende de ninguna otra capa.
- **Application** (`IProveedorSaludo.cs`, `ObtenerSaludoUseCase.cs`): define el
  puerto (interfaz) y el caso de uso. No conoce la implementación concreta.
- **Infrastructure** (`ProveedorSaludoEstatico.cs`): implementación concreta
  del puerto. Hoy devuelve un texto fijo; si mañana viniera de una API o DB,
  el cambio queda contenido acá.
- **Presentation**: app MAUI. `MainPage` solo conoce `ObtenerSaludoUseCase`
  (capa Application), nunca `Infrastructure` directamente — la resolución la
  hace el contenedor de Dependency Injection en `MauiProgram.cs`.

## Decisión técnica: colisión de namespace `Application`

`App.xaml.cs` tuvo que calificar el tipo base de MAUI como
`Microsoft.Maui.Controls.Application` (en vez de solo `Application`), porque
el proyecto `CocinaNadia.Application` (capa Clean Architecture)
comparte el namespace raíz `CocinaNadia` con `Presentation`, y el
compilador prioriza la resolución de namespace sobre el `using` de MAUI.
