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
    public class VisitaData : IDatos<Visita>
    {
        private readonly InmobiliariaContext _context;

        public VisitaData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);         
            _context = new InmobiliariaContext(optionsBuilder.Options);
        }

        public Respuesta<Visita> Actualizar(Visita entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Visita>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Visita> Eliminar(int id, bool logica = true)
        {
            var entidad = _context.Visita.Find(id);
            if (entidad == null)
                return new Respuesta<Visita>() { Completa = false, Datos = entidad ,Mensaje= "No existe el elemento que quiere eliminar" };

            _context.Remove(entidad);
            _context.SaveChanges();
            return new Respuesta<Visita>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Visita> Insertar(Visita entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Visita>() { Completa = true, Datos = entidad };
        }

        public List<Visita> Obtener(Expression<Func<Visita, bool>>? filtro = null)
        {
            IQueryable<Visita> ofertas = _context.Visita;
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Visita? Obtener(int id)
        {
            return _context?.Visita?.FirstOrDefault(x=>x.Id == id);
        }
    }
}
