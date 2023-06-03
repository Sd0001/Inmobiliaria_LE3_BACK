using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace test.Api
{
    public class PruebaInmuebles : TestBase
    {
        IDatos<Inmueble> datosInmueble;
        public PruebaInmuebles()
        {
            this.datosInmueble = new InmuebleData(_dbConfig);
        }
        public Respuesta<Inmueble> CrearInmueble()
        {

            ///Crear una nueva Inmueble
            var Inmueble = new Inmueble
            {
                
            };
            var Resultado = datosInmueble.Insertar(Inmueble);
            return Resultado;
        }
        public Respuesta<Inmueble> ActualizarInmueble(Inmueble Inmueble)
        {


            Inmueble.Apellido = "Quintero";
            return datosInmueble.Actualizar(Inmueble);

        }

        public Respuesta<Inmueble> EliminarInmueble(int id)
        {
            return datosInmueble.Eliminar(id);
        }
        /// <summary>
        /// Validar que se crea y se elimine una Inmueble
        /// </summary>
        [Fact]
        public void PruebaIntegralInmuebles()
        {
            var resultado = CrearInmueble();
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            Assert.True(resultado.Datos.Id > 0);

            //Prueba actualizar la Inmueble que se creo
            var InmuebleIns = resultado.Datos;
            resultado = ActualizarInmueble(InmuebleIns);
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            var resultado2 = datosInmueble.Obtener(resultado.Datos.Id);
            Assert.NotNull(resultado2);
            Assert.NotEqual(resultado2.Apellido, "zapata");

            //Prueba eliminar la Inmueble creada
            var resultado3 = EliminarInmueble(resultado.Datos.Id);
            Assert.True(resultado3.Completa);
            var resultado4 = datosInmueble.Obtener(resultado.Datos.Id);
            Assert.Null(resultado4);



        }

    }
}