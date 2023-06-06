namespace test.Api
{
    [Collection("Sequential")]
    public class PruebaSucursales : TestBase
    {
        IDatos<Sucursal> datosSucursal;

        public static Sucursal? created;

        public PruebaSucursales(string dbPruebas = "PruebaSucursales")
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseInMemoryDatabase(dbPruebas);
            var db = new InmobiliariaContext(optionsBuilder.Options);
            this.datosSucursal = new SucursalData(db);
            db.Estado.Add(new Estado { Id = 1, Nombre = "Activo" });
        }

        public Respuesta<Sucursal> CrearSucursal()
        {

            ///Crear una nueva Sucursal
            var Sucursal = new Sucursal
            {
                Id = 99,
                IdEstado = 1,
                Nombre = "prueba",
                Direccion = "calle61 #65",
                Telefono = "8879545",      
            };
            var resultado = datosSucursal.Insertar(Sucursal);
            created = resultado.Datos;
            return resultado;
        }
        public Respuesta<Sucursal> ActualizarSucursal(Sucursal Sucursal)
        {
            Sucursal.Nombre = "prueba2";
            return datosSucursal.Actualizar(Sucursal);
        }

        public Respuesta<Sucursal> EliminarSucursal()
        {
            return datosSucursal.Eliminar(created?.Id??0,false);
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
            Assert.NotEqual(resultado2.Nombre, "prueba");

            //Prueba eliminar la Sucursal creada
            var resultado3 = EliminarSucursal();
            Assert.True(resultado3.Completa);
            var resultado4 = datosSucursal.Obtener(resultado.Datos.Id);
            Assert.Null(resultado4);



        }

    }
}