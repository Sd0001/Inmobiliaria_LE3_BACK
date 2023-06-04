namespace Inmobiliaria.Entities.Interfaces
{
    public interface IDatos<T> : IDatosRead<T>
    {
        Respuesta<T> Actualizar(T entidad);

        Respuesta<T> Eliminar(int id, bool logica = true);

        Respuesta<T> Insertar(T entidad);
    }
}
