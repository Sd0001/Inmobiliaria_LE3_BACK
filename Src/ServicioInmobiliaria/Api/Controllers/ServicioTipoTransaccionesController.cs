using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
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

        [HttpGet]
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