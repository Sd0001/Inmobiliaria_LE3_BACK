using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace test.Api
{
    public class PruebaEstados : TestBase
    {
        /// <summary>
        /// Validar que hay 2 estados
        /// </summary>
        [Fact]
        public void CantidadEstados()
        {
            IDatosRead<Estado> datosEstado = new EstadoData(_dbConfig);
            var listaEstados = datosEstado.Obtener();
            // verificar que hay algún estado
            Assert.True(listaEstados.Any());
            int cantidadEstados = listaEstados.Count();
            //verificar que hay 2 estados
            Assert.Equal(2, cantidadEstados);
        }
    }
}