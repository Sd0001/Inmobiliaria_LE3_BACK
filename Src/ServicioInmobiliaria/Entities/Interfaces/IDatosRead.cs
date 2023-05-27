using System.Linq.Expressions;

namespace Inmobiliaria.Entities.Interfaces
{
    public interface IDatosRead<T>
    {
        List<T> Obtener(Expression<Func<T, bool>>? filtro = null);
        T? Obtener(int id);
    }
}