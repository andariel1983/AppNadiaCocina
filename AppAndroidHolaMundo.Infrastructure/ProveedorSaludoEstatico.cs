using AppAndroidHolaMundo.Application;
using AppAndroidHolaMundo.Domain;
using Microsoft.Extensions.Logging;

namespace AppAndroidHolaMundo.Infrastructure;

// Implementación concreta del puerto IProveedorSaludo.
// Acá vive el detalle técnico (en este caso trivial: un texto fijo).
// Si mañana el saludo viniera de una API o base de datos, el cambio queda
// contenido en esta capa: Application y Domain no se enteran.
public class ProveedorSaludoEstatico : IProveedorSaludo
{
    private readonly ILogger<ProveedorSaludoEstatico> _logger;

    public ProveedorSaludoEstatico(ILogger<ProveedorSaludoEstatico> logger)
    {
        _logger = logger;
    }

    public Saludo Obtener()
    {
        try
        {
            var saludo = new Saludo("¡Hola Mundo desde Android!");
            _logger.LogInformation("{Proveedor} devolvió el saludo estático", nameof(ProveedorSaludoEstatico));
            return saludo;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Falló {Proveedor} al construir el saludo", nameof(ProveedorSaludoEstatico));
            throw;
        }
    }
}
