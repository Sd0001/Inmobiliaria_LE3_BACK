using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioTipoTransaccionesController : ControllerBase, IServicioRead<TipoTransaccion>
    {
        private readonly IDatosRead<TipoTransaccion> _datosTipoTransaccion;
        private readonly ILogger<ServicioTipoTransaccionesController> _logger;
        public ServicioTipoTransaccionesController(ILogger<ServicioTipoTransaccionesController> logger,
            IDatosRead<TipoTransaccion> datosTipoTransaccion)
        {
            _logger = logger;
            _datosTipoTransaccion = datosTipoTransaccion;
        }

        /// <summary>
        /// Obtiene la lista de tipos de transacciones de la aplicación.
        /// </summary>
        /// <returns>Respuesta con la lista de tipos de transacciones.</returns>
        /// <response code="200">La lista de tipos de transacciones se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<TipoTransaccion>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<TipoTransaccion>>), (int)HttpStatusCode.InternalServerError)]
        public Respuesta<IEnumerable<TipoTransaccion>> Listar()
        {
            try
            {
                var model = _datosTipoTransaccion.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<TipoTransaccion>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<TipoTransaccion>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}