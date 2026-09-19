using AppAndroidHolaMundo.Domain;
using Microsoft.Extensions.Logging;

namespace AppAndroidHolaMundo.Application;

// Caso de uso: orquesta la lógica de aplicación (acá es trivial, pero es el lugar
// correcto para crecer sin mezclar reglas de negocio con la UI).
public class ObtenerSaludoUseCase
{
    private readonly IProveedorSaludo _proveedorSaludo;
    private readonly ILogger<ObtenerSaludoUseCase> _logger;

    public ObtenerSaludoUseCase(IProveedorSaludo proveedorSaludo, ILogger<ObtenerSaludoUseCase> logger)
    {
        _proveedorSaludo = proveedorSaludo;
        _logger = logger;
    }

    public Saludo Ejecutar()
    {
        _logger.LogInformation("Ejecutando caso de uso {CasoDeUso}", nameof(ObtenerSaludoUseCase));

        try
        {
            var saludo = _proveedorSaludo.Obtener();
            _logger.LogInformation("Caso de uso {CasoDeUso} resuelto: \"{Mensaje}\"", nameof(ObtenerSaludoUseCase), saludo.Mensaje);
            return saludo;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Falló {CasoDeUso} al pedirle el saludo a {Proveedor}", nameof(ObtenerSaludoUseCase), _proveedorSaludo.GetType().Name);
            throw;
        }
    }
}
