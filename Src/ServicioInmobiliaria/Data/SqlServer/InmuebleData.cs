using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace Inmobiliaria.Data.SqlServer
{
    public class InmuebleData : IDatos<Inmueble>
    {
        public Respuesta<int> actuallizar(Inmueble entidad)
        {
            throw new NotImplementedException();
        }

        public Respuesta<int> Insertar(Inmueble entidad)
        {
            throw new NotImplementedException();
        }

        public Inmueble Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public List<Inmueble> Obtener(Func<Inmueble, bool> filtro)
        {
            throw new NotImplementedException();
        }
    }
}
