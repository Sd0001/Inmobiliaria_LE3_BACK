namespace Inmobiliaria.Entities.Interfaces
{
    public interface IServicio<T>: IServicioRead<T>
    {
        Respuesta<T> Actualizar(T model);

        Respuesta<T> Eliminar(int id);

        public Respuesta<T> Insertar(T model);
    }

}
