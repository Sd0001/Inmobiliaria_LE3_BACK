using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inmobiliaria.Data.SqlServer
{
    public class InmuebleData : IDatos<Inmueble>
    {
        private readonly InmobiliariaContext _context;

        public InmuebleData(InmobiliariaContext context)
        {
            _context = context;
        }

        public Respuesta<Inmueble> Actualizar(Inmueble entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Inmueble>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Inmueble> Eliminar(int id, bool logica = true)
        {
            var entidad = _context.Inmueble.Find(id);
            if (entidad == null)
                return new Respuesta<Inmueble>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };
            if (logica)
            {
                //_context.Remove(entidad);
                entidad.IdEstado = 2;
                _context.Update(entidad);
                _context.SaveChanges();
            }
            else
            {
                _context.Remove(entidad);
                _context.SaveChanges();
            }
            return new Respuesta<Inmueble>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Inmueble> Insertar(Inmueble entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Inmueble>() { Completa = true, Datos = entidad };
        }

        public List<Inmueble> Obtener(Expression<Func<Inmueble, bool>>? filtro = null)
        {
            IQueryable<Inmueble> ofertas = _context.Inmueble
                    .Include(x => x.Estado)
                    .Include(x => x.Sucursal)
                    .Include(x => x.TipoInmueble)
                    .Include(x => x.Persona);
            if (filtro != null)
                ofertas = ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Inmueble? Obtener(int id)
        {
            return _context?.Inmueble?
                    .Include(x => x.Estado)
                    .Include(x => x.Sucursal)
                    .Include(x => x.TipoInmueble)
                    .Include(x => x.Persona)
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
