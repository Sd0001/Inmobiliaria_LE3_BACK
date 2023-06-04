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
    public class PersonaData : IDatos<Persona>
    {
        private readonly InmobiliariaContext _context;

        public PersonaData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);
            _context = new InmobiliariaContext(optionsBuilder.Options);
        }

        public Respuesta<Persona> Actualizar(Persona entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Persona>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Persona> Eliminar(int id, bool logica = true)
        {
            var entidad = _context.Persona.Find(id);
            if (entidad == null)
                return new Respuesta<Persona>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            _context.Remove(entidad);
            _context.SaveChanges();
            return new Respuesta<Persona>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Persona> Insertar(Persona entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Persona>() { Completa = true, Datos = entidad };
        }

        public List<Persona> Obtener(Expression<Func<Persona, bool>>? filtro = null)
        {
            IQueryable<Persona> ofertas = _context.Persona;
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Persona? Obtener(int id)
        {
            return _context?.Persona?.FirstOrDefault(x=>x.Id == id);
        }
    }
}
