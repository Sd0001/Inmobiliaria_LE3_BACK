namespace Inmobiliaria.Entities.Interfaces
{
    public interface IDatosRead<T>
    {
        List<T> Obtener(Func<T, bool> filtro);
        T Obtener(int id);
    }
}
