using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inmobiliaria.Data.SqlServer
{
    public class SucursalData : IDatos<Sucursal>
    {
        private readonly InmobiliariaContext _context;

        public SucursalData(InmobiliariaContext context)
        {
            _context = context;
        }

        public Respuesta<Sucursal> Actualizar(Sucursal entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Sucursal>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Sucursal> Eliminar(int id, bool logica = true)
        {
            var entidad = _context.Sucursal.Find(id);
            if (entidad == null)
                return new Respuesta<Sucursal>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };
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
            return new Respuesta<Sucursal>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Sucursal> Insertar(Sucursal entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Sucursal>() { Completa = true, Datos = entidad };
        }

        public List<Sucursal> Obtener(Expression<Func<Sucursal, bool>>? filtro = null)
        {
            IQueryable<Sucursal> ofertas = _context.Sucursal .Include(x=>x.Estado);
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Sucursal? Obtener(int id)
        {
            return _context?.Sucursal?.Include(x => x.Estado).FirstOrDefault(x => x.Id == id);
        }
    }
}
