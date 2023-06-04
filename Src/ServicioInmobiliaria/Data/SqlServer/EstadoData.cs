using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using System.Linq.Expressions;

namespace Inmobiliaria.Data.SqlServer
{
    public class EstadoData : IDatosRead<Estado>
    {
        private readonly InmobiliariaContext _context;

        public EstadoData(InmobiliariaContext context)
        {
            _context = context;
        }
       
        public List<Estado> Obtener(Expression<Func<Estado, bool>>? filtro = null)
        {
            IQueryable<Estado> ofertas = _context.Estado;
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Estado? Obtener(int id)
        {
            return _context?.Estado?.FirstOrDefault(x=>x.Id == id);
        }
    }
}
