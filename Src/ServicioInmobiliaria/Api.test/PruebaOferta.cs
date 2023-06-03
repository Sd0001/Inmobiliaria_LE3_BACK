using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace test.Api
{
    public class PruebaOferta : TestBase
    {
        IDatos<Oferta> datosOferta;
        private PruebaInmuebles pruebaInmuebles;

        public PruebaOferta()
        {

            this.datosOferta = new OfertaData(_dbConfig);
            this.pruebaInmuebles = new PruebaInmuebles();
        }
        public Respuesta<Oferta> CrearOferta()
        {
            var inmueble = pruebaInmuebles.CrearInmueble();


            ///Crear una nueva Oferta
            var Oferta = new Oferta
            {
                IdEstado = 1,
                FechaInicio = DateTime.Now,
                Fechafin = DateTime.Now.AddDays(4),
                MontoVenta = 100000000,
                EsAlquiler = true,
                EsVenta = false,
                Montoalquiler= 1000000,
                IdInmueble = inmueble?.Datos?.Id??0,
               

            };
            var Resultado = datosOferta.Insertar(Oferta);
            return Resultado;
        }
        public Respuesta<Oferta> ActualizarOferta(Oferta Oferta)
        {

            Oferta.Montoalquiler = 500000;
           
            return datosOferta.Actualizar(Oferta);

        }

        public Respuesta<Oferta> EliminarOferta(Oferta oferta)
        {

           

            var resultado = datosOferta.Eliminar(oferta.Id);
            var inmueble = pruebaInmuebles.EliminarInmueble(oferta.IdInmueble);
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
            Assert.NotEqual(resultado2.Montoalquiler, 500000);

            //Prueba eliminar la Oferta creada
            var resultado3 = EliminarOferta(resultado.Datos.);
            Assert.True(resultado3.Completa);
            var resultado4 = datosOferta.Obtener(resultado.Datos.Id);
            Assert.Null(resultado4);



        }

    }
}