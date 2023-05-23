using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioEstadosController : ControllerBase, IServicioRead<Estado>
    {
        private readonly IDatosRead<Estado> _datosEstado;
        private readonly ILogger<ServicioEstadosController> _logger;
        public ServicioEstadosController(ILogger<ServicioEstadosController> logger,
            IDatosRead<Estado> datosEstado)
        {
            _logger = logger;
            _datosEstado = datosEstado;
        }

        /// <summary>
        /// Método  para obtener la lista de estados de la aplicación
        /// </summary>
        /// <remarks>
        /// Método  para obtener la lista de estados de la aplicación <br/>
        /// 1 => 'Activo' <br/>
        /// 2 => 'Eliminado'
        /// <response code="200">El estado se elimin� correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<IEnumerable<Estado>> Listar()
        {
            try
            {
                var model = _datosEstado.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Estado>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Estado>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}