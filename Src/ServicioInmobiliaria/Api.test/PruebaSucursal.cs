using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace test.Api
{
    public class PruebaSucursales : TestBase
    {
        IDatos<Sucursal> datosSucursal;
        public PruebaSucursales()
        {
            this.datosSucursal = new SucursalData(_dbConfig);
        }
        public Respuesta<Sucursal> CrearSucursal()
        {

            ///Crear una nueva Sucursal
            var Sucursal = new Sucursal
            {
                IdEstado = 1,
                Nombre = "prueba",
                Direccion = "calle61 #65",
                Telefono = "8879545",
                
               
            };
            var Resultado = datosSucursal.Insertar(Sucursal);
            return Resultado;
        }
        public Respuesta<Sucursal> ActualizarSucursal(Sucursal Sucursal)
        {


            Sucursal.Nombre = "prueba2";
            return datosSucursal.Actualizar(Sucursal);

        }

        public Respuesta<Sucursal> EliminarSucursal(int id)
        {
            return datosSucursal.Eliminar(id);
        }
        /// <summary>
        /// Validar que se crea y se elimine una Sucursal
        /// </summary>
        [Fact]
        public void PruebaIntegralSucursals()
        {
            var resultado = CrearSucursal();
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            Assert.True(resultado.Datos.Id > 0);

            //Prueba actualizar la Sucursal que se creo
            var SucursalIns = resultado.Datos;
            resultado = ActualizarSucursal(SucursalIns);
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            var resultado2 = datosSucursal.Obtener(resultado.Datos.Id);
            Assert.NotNull(resultado2);
            Assert.NotEqual(resultado2.Nombre, "prueba2");

            //Prueba eliminar la Sucursal creada
            var resultado3 = EliminarSucursal(resultado.Datos.Id);
            Assert.True(resultado3.Completa);
            var resultado4 = datosSucursal.Obtener(resultado.Datos.Id);
            Assert.Null(resultado4);



        }

    }
}