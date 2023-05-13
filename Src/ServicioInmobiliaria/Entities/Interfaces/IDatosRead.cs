namespace Inmobiliaria.Entities.Interfaces
{
    public interface IDatosRead<T>
    {
        List<T> Obtener(Func<T, bool>? filtro = null);
        T? Obtener(int id);
    }
}
