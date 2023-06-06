
namespace test.Api
{  
    [Collection("Sequential")]
    public class PruebaEstados : TestBase
    {
        private readonly string dbPruebas;

        public PruebaEstados(string dbPruebas = "PruebaEstados")
        {
            this.dbPruebas = dbPruebas;
        }

        /// <summary>
        /// Validar que hay 2 estados
        /// </summary>
        [Fact]
        public void CantidadEstados()
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseInMemoryDatabase(dbPruebas);
            var db = new InmobiliariaContext(optionsBuilder.Options);
            db.Estado.Add(new Estado { Id = 1, Nombre = "Activo" });
            db.Estado.Add(new Estado { Id = 2, Nombre = "Eliminado" });
            db.SaveChanges();

            IDatosRead<Estado> datosEstado = new EstadoData(db);
            var listaEstados = datosEstado.Obtener();
            // verificar que hay algún estado
            Assert.True(listaEstados.Any());
            int cantidadEstados = listaEstados.Count();
            //verificar que hay 2 estados
            Assert.Equal(2, cantidadEstados);
        }
    }
}