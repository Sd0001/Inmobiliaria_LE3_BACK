using Inmobiliaria.Data.SqlServer;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;

namespace test.Api
{
    public class PruebaPersonas : TestBase
    {
        IDatos<Persona> datosPersona;

        public static Persona? created;

        public PruebaPersonas() { 
           this.datosPersona = new PersonaData(_dbConfig);
        }
        public Respuesta<Persona> CrearPersona() {
           
            ///Crear una nueva persona
            var persona = new Persona
            {
                Apellido = "zapata",
                Nombre = "Jhon",
                Identificacion = "999999999",
                Direccion = "calle61 #34",
                Password = "1234",
                Email = "alejo.zapata",
                Telefono = "8879545"
            };
            var resultado = datosPersona.Insertar(persona);
            created = resultado.Datos;
            return resultado;
        }
        public Respuesta<Persona> ActualizarPersona(Persona persona)
        {

           
            persona.Apellido = "Quintero";
            return  datosPersona.Actualizar(persona);

        }

        public Respuesta<Persona> EliminarPersona()
        {
            return datosPersona.Eliminar(created.Id, false);
        }
        /// <summary>
        /// Validar que se crea y se elimine una persona
        /// </summary>
        [Fact]
        public void PruebaIntegralPersonas()
        {
            var resultado = CrearPersona();
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            Assert.True(resultado.Datos.Id > 0);

            //Prueba actualizar la persona que se creo
            var personaIns = resultado.Datos;
            resultado= ActualizarPersona(personaIns);
            Assert.True(resultado.Completa);
            Assert.NotNull(resultado.Datos);
            var resultado2 = datosPersona.Obtener(resultado.Datos.Id);
            Assert.NotNull(resultado2);
            Assert.NotEqual(resultado2.Apellido, "zapata");

            //Prueba eliminar la persona creada
            var resultado3 = EliminarPersona();
            Assert.True(resultado3.Completa);
            var resultado4 = datosPersona.Obtener(resultado.Datos.Id);
            Assert.Null(resultado4);



        }

    }
}