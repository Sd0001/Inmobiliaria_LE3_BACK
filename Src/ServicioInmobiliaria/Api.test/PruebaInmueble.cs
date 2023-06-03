using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace test.Api
{
    public class PruebaInmuebles : TestBase
    {
        IDatos<Inmueble> datosInmueble;
        private PruebaSucursales pruebaSucursales;
        private PruebaPersonas pruebaPersonas;
        public PruebaInmuebles()
        {
            this.datosInmueble = new InmuebleData(_dbConfig);
            this.pruebaSucursales = new PruebaSucursales();
            this.pruebaPersonas= new PruebaPersonas();
        }
        public Respuesta<Inmueble> CrearInmueble()
        {
            var sucursal = pruebaSucursales.CrearSucursal();
            var persona = pruebaPersonas.CrearPersona();

            ///Crear una nueva Inmueble
            var Inmueble = new Inmueble
            {
                IdSucursal = sucursal?.Datos?.Id ?? 0,
                Superficie = 50,
                Direccion = "calle 64 #54",
                NroBanios= 1,
                NroCocinas= 1,
                NroHabitaciones= 3, 
                TieneGas= true,
                TieneParqueadero= true, 
                Referencia= "3434343",
                IdEstado= 1,
                IdTipoInmueble= 1, 
                IdPersona= persona?.Datos?.Id ?? 0,

            };
            var Resultado = datosInmueble.Insertar(Inmueble);
            return Resultado;
        }
        public Respuesta<Inmueble> ActualizarInmueble(Inmueble Inmueble)
        {


            Inmueble.Superficie = 100;
            return datosInmueble.Actualizar(Inmueble);

        }

        public Respuesta<Inmueble> EliminarInmueble(Inmueble inmueble)
        {

            var resultado = datosInmueble.Eliminar(inmueble.Id);
            var persona = pruebaPersonas.EliminarPersona(inmueble.IdPersona);
            var sucursal = pruebaSucursales.EliminarSucursal(inmueble.IdSucursal);
            return resultado;
            
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