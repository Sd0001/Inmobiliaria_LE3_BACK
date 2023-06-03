using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Data.SqlServer
{
    public class TransaccionData : IDatos<Transaccion>
    {
        private readonly InmobiliariaContext _context;

        public TransaccionData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);
            _context = new InmobiliariaContext(optionsBuilder.Options);
        }

        public Respuesta<Transaccion> Actualizar(Transaccion entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Transaccion>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Transaccion> Eliminar(int id)
        {
            var entidad = _context.Transaccion.Find(id);
            if (entidad == null)
                return new Respuesta<Transaccion>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            //_context.Remove(entidad);
             entidad.IdEstado=2;
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Transaccion>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Transaccion> Insertar(Transaccion entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Transaccion>() { Completa = true, Datos = entidad };
        }

        public List<Transaccion> Obtener(Expression<Func<Transaccion, bool>>? filtro = null)
        {
            IQueryable<Transaccion> ofertas = _context.Transaccion
                .Include(x => x.Estado)
                .Include(x => x.Oferta).ThenInclude(x => x.Inmueble).ThenInclude(x => x.Persona)
                .Include(x => x.Oferta).ThenInclude(x => x.Inmueble).ThenInclude(x => x.TipoInmueble)
                .Include(x => x.TipoTransaccion)
                .Include(x => x.Persona);
            if (filtro != null)
                ofertas = ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Transaccion? Obtener(int id)
        {
            return _context?.Transaccion?
                .Include(x => x.Estado)
                .Include(x => x.Oferta).ThenInclude(x => x.Inmueble).ThenInclude(x => x.Persona)
                .Include(x => x.Oferta).ThenInclude(x => x.Inmueble).ThenInclude(x => x.TipoInmueble)
                .Include(x => x.TipoTransaccion)
                .Include(x => x.Persona)
                .FirstOrDefault(x=>x.Id == id);
        }

        private static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, bool> f)
        {
            return x => f(x);
        }
    }
}
