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
    public class TipoInmuebleData : IDatos<TipoInmueble>
    {
        private readonly InmobiliariaContext _context;

        public TipoInmuebleData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);
            _context = new InmobiliariaContext(optionsBuilder.Options);
        }

        public Respuesta<TipoInmueble> Actualizar(TipoInmueble entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<TipoInmueble>() { Completa = true, Datos = entidad };
        }

        public Respuesta<TipoInmueble> Eliminar(int id)
        {
            var entidad = _context.TipoInmueble.Find(id);
            if (entidad == null)
                return new Respuesta<TipoInmueble>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            _context.Remove(entidad);
            _context.SaveChanges();
            return new Respuesta<TipoInmueble>() { Completa = true, Datos = entidad };
        }

        public Respuesta<TipoInmueble> Insertar(TipoInmueble entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<TipoInmueble>() { Completa = true, Datos = entidad };
        }

        public List<TipoInmueble> Obtener(Expression<Func<TipoInmueble, bool>>? filtro = null)
        {
            IQueryable<TipoInmueble> ofertas = _context.TipoInmueble;
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public TipoInmueble? Obtener(int id)
        {
            return _context?.TipoInmueble?.FirstOrDefault(x=>x.Id == id);
        }
    }
}
