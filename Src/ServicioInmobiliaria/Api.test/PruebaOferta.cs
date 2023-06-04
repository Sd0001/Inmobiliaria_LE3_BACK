using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace test.Api
{
    public class PruebaOferta : TestBase
    {
        IDatos<Oferta> datosOferta;
        private PruebaInmuebles pruebaInmuebles;

        public static Oferta? created;

        public PruebaOferta()
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseInMemoryDatabase("Inmobiliaria");
            var db = new InmobiliariaContext(optionsBuilder.Options);
            this.datosOferta = new OfertaData(db);
            this.pruebaInmuebles = new PruebaInmuebles();
        }
        public Respuesta<Oferta> CrearOferta()
        {
            var inmueble = pruebaInmuebles.CrearInmueble();
            Persona? persona;
         
            ///Crear una nueva Oferta
            var Oferta = new Oferta
                {
                    IdEstado = 1,
                    FechaInicio = DateTime.Now,
                    Fechafin = DateTime.Now.AddDays(4),
                    MontoVenta = 100000000,
                    EsAlquiler = true,
                    EsVenta = false,
                    Montoalquiler = 1000000,
                    IdInmueble = inmueble?.Datos?.Id ?? 0,
                };
            var resultado = datosOferta.Insertar(Oferta);
            created = resultado?.Datos;
            return resultado;
        }
        public Respuesta<Oferta> ActualizarOferta(Oferta Oferta)
        {
            Oferta.Montoalquiler = 500000;
            return datosOferta.Actualizar(Oferta);
        }

        public Respuesta<Oferta> EliminarOferta()
        {
            var resultado = datosOferta.Eliminar(created.Id,false);
            var inmueble = pruebaInmuebles.EliminarInmueble();
            return resultado;
        }
        /// <summary>
        /// Validar que se crea y se elimine una Oferta
        /// </summary>
        [Fact]
        public void PruebaIntegralOfertas()
        {
            var resultado = CrearOferta();
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            Assert.True(resultado.Datos.Id > 0);

            //Prueba actualizar la Oferta que se creo
            var OfertaIns = resultado.Datos;
            resultado = ActualizarOferta(OfertaIns);
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            var resultado2 = datosOferta.Obtener(resultado.Datos.Id);
            Assert.NotNull(resultado2);
            Assert.NotEqual(resultado2.Montoalquiler, 100000000);

            //Prueba eliminar la Oferta creada
            var resultado3 = EliminarOferta();
            Assert.True(resultado3.Completa);
            var resultado4 = datosOferta.Obtener(resultado.Datos.Id);
            Assert.Null(resultado4);
        }
    }
}