using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    /// <summary>
    /// servicio de estados
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ServicioUsuariosController : ControllerBase, IServicioUsuarios
    {
        private readonly IDatos<Persona> _datosPersona;
        private readonly ILogger<ServicioUsuariosController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServicioUsuariosController(ILogger<ServicioUsuariosController> logger,
            IDatos<Persona> datosPersona)
        {
            _logger = logger;
            _datosPersona = datosPersona;
        }

        /// <summary>
        /// Método para validar un usuario
        /// </summary>
        /// <remarks>
        /// Método para validar un usuario
        /// <response code="200">Credenciales correctas.</response>
        /// <response code="401">Credenciales incorrectas.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<bool> Validar(string usuario, string contrasena)
        {
            try
            {
                var conectado = _datosPersona.Obtener(x => x.Email == usuario && x.Password == contrasena).Any();//TODO consumir datos
                if (conectado)
                    this.Response.StatusCode = (int)HttpStatusCode.OK;
                else
                    this.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return new Respuesta<bool> { Completa = true, Mensaje = "", Datos = conectado };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<bool> { Completa = false, Mensaje = ex.Message, Datos = false };
            }
        }
    }
}