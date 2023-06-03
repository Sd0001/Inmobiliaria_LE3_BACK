using Inmobiliaria.Entities;

namespace test.Api
{
    public abstract class TestBase
    {
        /// <summary>
        /// Configuración del acceso a datos para parar a los métodos
        /// </summary>
        protected DbConfig _dbConfig;

        public TestBase()
        {
            var connectionString = "data source=localhost;initial catalog=inmobiliria;MultipleActiveResultSets=True;App=EntityFramework;Integrated Security=true;Encrypt=False";
            var dbConfig = new DbConfig();
            dbConfig.ConnectionString = connectionString ?? "";
            _dbConfig = dbConfig;
        }
    }
}