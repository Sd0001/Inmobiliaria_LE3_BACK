namespace Inmobiliaria.Entities.Interfaces
{
    public interface IServicioRead<T>
    {
        Respuesta<IEnumerable<T>> Listar();
    }

}
