using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using System.Linq.Expressions;

namespace Inmobiliaria.Data.SqlServer
{
    public class TipoTransaccionData : IDatosRead<TipoTransaccion>
    {
        private readonly InmobiliariaContext _context;

        public TipoTransaccionData(InmobiliariaContext context)
        {
            _context = context;
        }

        public List<TipoTransaccion> Obtener(Expression<Func<TipoTransaccion, bool>>? filtro = null)
        {
            IQueryable<TipoTransaccion> ofertas = _context.TipoTransaccion;
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public TipoTransaccion? Obtener(int id)
        {
            return _context?.TipoTransaccion?.FirstOrDefault(x=>x.Id == id);
        }
    }
}
