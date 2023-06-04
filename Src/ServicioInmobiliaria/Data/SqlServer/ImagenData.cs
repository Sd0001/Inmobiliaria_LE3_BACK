using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using System.Linq.Expressions;

namespace Inmobiliaria.Data.SqlServer
{
    public class ImagenData : IDatos<Imagen>
    {
        private readonly InmobiliariaContext _context;

        public ImagenData(InmobiliariaContext context)
        {
            _context = context;
        }

        public Respuesta<Imagen> Actualizar(Imagen entidad)
        {
            _context.Update(entidad);
            _context.SaveChanges();
            return new Respuesta<Imagen>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Imagen> Eliminar(int id, bool logica = true)
        {
            var entidad = _context.Imagen.Find(id);
            if (entidad == null)
                return new Respuesta<Imagen>() { Completa = false, Datos = entidad, Mensaje = "No existe el elemento que quiere eliminar" };

            _context.Remove(entidad);
            _context.SaveChanges();
            return new Respuesta<Imagen>() { Completa = true, Datos = entidad };
        }

        public Respuesta<Imagen> Insertar(Imagen entidad)
        {
            _context.Add(entidad);
            _context.SaveChanges();
            return new Respuesta<Imagen>() { Completa = true, Datos = entidad };
        }

        public List<Imagen> Obtener(Expression<Func<Imagen, bool>>? filtro = null)
        {
            IQueryable<Imagen> ofertas = _context.Imagen;
            if (filtro != null)
                ofertas = ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Imagen? Obtener(int id)
        {
            return _context?.Imagen?.FirstOrDefault(x => x.Id == id);
        }
    }
}
