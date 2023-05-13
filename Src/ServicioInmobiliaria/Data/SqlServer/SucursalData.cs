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

        public Respuesta<Sucursal> Eliminar(int id)
        {
            var entidad = _context.Sucursal.Find(id);
            if (entidad == null)
                return new Respuesta<Sucursal>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            _context.Remove(entidad);
            _context.SaveChanges();
            return new Respuesta<Sucursal>() { Completa = true, Datos = entidad };
        }


        public Respuesta<Sucursal> Insertar(Sucursal entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Sucursal>() { Completa = true, Datos = entidad };
        }

        public List<Sucursal> Obtener(Func<Sucursal, bool>? filtro = null)
        {
            IQueryable<Sucursal> ofertas = _context.Sucursal;
            if (filtro != null)
                ofertas=  ofertas.Where(FuncToExpression(filtro));
            return ofertas.ToList();
        }

        public Sucursal? Obtener(int id)
        {
            return _context?.Sucursal?.FirstOrDefault(x=>x.Id == id);
        }

        private static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, bool> f)
        {
            return x => f(x);
        }
    }
}
