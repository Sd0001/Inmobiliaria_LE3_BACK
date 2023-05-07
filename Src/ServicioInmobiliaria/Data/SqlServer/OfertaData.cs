using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Data.SqlServer
{
    public class OfertaData : IDatos<Oferta>
    {
        public Respuesta<int> actuallizar(Oferta entidad)
        {
            throw new NotImplementedException();
        }

        public Respuesta<int> Insertar(Oferta entidad)
        {
            throw new NotImplementedException();
        }

        public List<Oferta> Obtener(Func<Oferta, bool> filtro)
        {
            throw new NotImplementedException();
        }

        public Oferta Obtener(int id)
        {
            throw new NotImplementedException();
        }
    }
}
