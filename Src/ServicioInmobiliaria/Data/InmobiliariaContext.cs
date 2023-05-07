using Inmobiliaria.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inmobilliaria.Data
{
    public class InmobiliariaContext : DbContext
    {
        public InmobiliariaContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Para las vistas
            //modelBuilder.Entity<Componentes>().HasNoKey();
           
        }

        public DbSet<Estado> Estado { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Inmueble> Inmueble { get; set; }
        public DbSet<Oferta> Oferta { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<TipoInmueble> TipoInmueble { get; set; }
        public DbSet<TipoTransaccion> TipoTransaccion { get; set; }
        public DbSet<Transaccion> Transaccion { get; set; }
        public DbSet<Visita> Visita { get; set; }
    }
}