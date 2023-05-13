using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inmobiliaria.Data.SqlServer
{
    public class OfertaData : IDatos<Oferta>
    {
        private readonly InmobiliariaContext _context;

        public OfertaData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);
            _context = new InmobiliariaContext(optionsBuilder.Options);
        }

        public Respuesta<Oferta> Actualizar(Oferta entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Oferta>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Oferta> Eliminar(int id)
        {
            var entidad = _context.Oferta.Find(id);
            if (entidad == null)
                return new Respuesta<Oferta>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            _context.Remove(entidad);
            _context.SaveChanges();
            return new Respuesta<Oferta>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Oferta> Insertar(Oferta entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Oferta>() { Completa = true, Datos = entidad };
        }

        public List<Oferta> Obtener(Func<Oferta, bool>? filtro = null)
        {
            IQueryable<Oferta> ofertas = _context.Oferta;
            if (filtro != null)
                ofertas=  ofertas.Where(FuncToExpression(filtro));
            return ofertas.ToList();
        }

        public Oferta? Obtener(int id)
        {
            return _context?.Oferta?.FirstOrDefault(x=>x.Id == id);
        }

        private static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, bool> f)
        {
            return x => f(x);
        }
    }
}
