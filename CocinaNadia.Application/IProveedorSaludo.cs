using CocinaNadia.Domain;

namespace CocinaNadia.Application;

// Puerto (interfaz) que define QUÉ necesita la aplicación, sin decir CÓMO se resuelve.
// La capa Infrastructure implementa esto; Application no conoce esa implementación
// (Dependency Inversion: las capas internas no dependen de las externas).
public interface IProveedorSaludo
{
    Saludo Obtener();
}
