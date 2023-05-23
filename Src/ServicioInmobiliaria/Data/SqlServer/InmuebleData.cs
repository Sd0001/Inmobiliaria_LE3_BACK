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
    public class InmuebleData : IDatos<Inmueble>
    {
        private readonly InmobiliariaContext _context;

        public InmuebleData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);
            _context = new InmobiliariaContext(optionsBuilder.Options);
        }

        public Respuesta<Inmueble> Actualizar(Inmueble entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Inmueble>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Inmueble> Eliminar(int id)
        {
            var entidad = _context.Inmueble.Find(id);
            if (entidad == null)
                return new Respuesta<Inmueble>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            //_context.Remove(entidad);
            entidad.IdEstado=2;
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Inmueble>() { Completa = true, Datos = entidad };
        }
        public Respuesta<Inmueble> Insertar(Inmueble entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Inmueble>() { Completa = true, Datos = entidad };
        }
               

        public List<Inmueble> Obtener(Func<Inmueble, bool>? filtro = null)
        {
            IQueryable<Inmueble> ofertas = _context.Inmueble;
            if (filtro != null)
                ofertas=  ofertas.Where(FuncToExpression(filtro));
            return ofertas.ToList();
        }

        public Inmueble? Obtener(int id)
        {
            return _context?.Inmueble?.FirstOrDefault(x=>x.Id == id);
        }

        private static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, bool> f)
        {
            return x => f(x);
        }
    }
}
