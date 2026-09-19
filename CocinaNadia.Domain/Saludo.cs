namespace CocinaNadia.Domain;

// Entidad de dominio: representa el concepto de negocio "saludo".
// No depende de ninguna otra capa (regla de Clean Architecture: el dominio es el núcleo).
public class Saludo
{
    public string Mensaje { get; }

    public Saludo(string mensaje)
    {
        if (string.IsNullOrWhiteSpace(mensaje))
            throw new ArgumentException("El mensaje no puede estar vacío.", nameof(mensaje));

        Mensaje = mensaje;
    }
}
