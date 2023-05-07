namespace Inmobiliaria.Entities.Interfaces
{
    public interface IDatos<T> : IDatosRead<T>
    {
        Respuesta<int> Insertar(T entidad);
        Respuesta<int> actuallizar(T entidad);
    }
}
