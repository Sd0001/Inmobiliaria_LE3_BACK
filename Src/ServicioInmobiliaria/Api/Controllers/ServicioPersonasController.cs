using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobiliaria.Api.Modules;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
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
        /// <summary>
        /// Método para actualizar a una persona
        /// </summary>
        /// <param name="model">Datos de la persona a actualizar.</param>
        /// <response code="200">La persona se actualizó correctamente.</response>
        /// <response code="304">la persona no se pudo actualizar debido a datos no modificados.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se actualiza nos datos de una persona <br/>
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        [TypeFilter(typeof(AuthorizeActionFilter))]
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
        /// <summary>
        /// Método para eliminar a una persona
        /// </summary>
        /// <param name="id">ID del tipo de persona a eliminar.</param>
        /// <returns>Respuesta con los resultados de la eliminación.</returns>
        /// <response code="200">La persona se eliminó correctamente.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        /// <response code="404">La persona no se encontró.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se elimina una persona <br/>
        /// </remarks>

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        [TypeFilter(typeof(AuthorizeActionFilter))]
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
        /// <summary>
        /// Método para crear a una persona
        /// </summary>
        /// <param name="model">Datos de la persona a insertar.</param>
        /// <response code="200">La persona se inserto correctamente.</response>
        /// <response code="304">la persona no se pudo insertar debido a datos no modificados.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se crea una persona<br/>
        /// </remarks>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        [TypeFilter(typeof(AuthorizeActionFilter))]
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
        /// <summary>
        /// Método para consultar una persona
        /// </summary>
        /// <response code="200">La persona se obtuvo correctamente.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se consulta una persona <br/>
        /// </remarks>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        [TypeFilter(typeof(AuthorizeActionFilter))]
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