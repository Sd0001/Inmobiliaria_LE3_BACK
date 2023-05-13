using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioPersonasController : ControllerBase, IServicio<Persona>
    {
        private readonly IDatos<Persona> _datosPersona;
        private readonly ILogger<ServicioPersonasController> _logger;
        public ServicioPersonasController(ILogger<ServicioPersonasController> logger,
            IDatos<Persona> datosPersona)
        {
            _logger = logger;
            _datosPersona = datosPersona;
        }

        [HttpPut]
        public Respuesta<Persona> Actualizar(Persona model)
        {
            try
            {
                var respuesta = _datosPersona.Actualizar(model);//TODO consumir datos
                if (respuesta.Completa)
                    this.Response.StatusCode = (int)HttpStatusCode.OK;
                else
                    this.Response.StatusCode = (int)HttpStatusCode.NotModified;
                return respuesta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<Persona> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpDelete]
        public Respuesta<Persona> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosPersona.Eliminar(id);//TODO consumir datos
                if (respuesta.Completa)
                    this.Response.StatusCode = (int)HttpStatusCode.OK;
                else
                    this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return respuesta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<Persona> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpPost]
        public Respuesta<Persona> Insertar(Persona model)
        {
            try
            {
                var respuesta = _datosPersona.Insertar(model);//TODO consumir datos              
                if (respuesta.Completa)
                    this.Response.StatusCode = (int)HttpStatusCode.OK;
                else
                    this.Response.StatusCode = (int)HttpStatusCode.NotModified;
                return respuesta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<Persona> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpGet]
        public Respuesta<IEnumerable<Persona>> Listar()
        {
            try
            {
                var model = _datosPersona.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Persona>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Persona>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}