using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioTipoTransaccionesController : ControllerBase, IServicio<TipoTransaccion>
    {
        private readonly IDatos<TipoTransaccion> _datosTipoTransaccion;
        private readonly ILogger<ServicioTipoTransaccionesController> _logger;
        public ServicioTipoTransaccionesController(ILogger<ServicioTipoTransaccionesController> logger,
            IDatos<TipoTransaccion> datosTipoTransaccion)
        {
            _logger = logger;
            _datosTipoTransaccion = datosTipoTransaccion;
        }

        [HttpPut]
        public Respuesta<TipoTransaccion> Actualizar(TipoTransaccion model)
        {
            try
            {
                var respuesta = _datosTipoTransaccion.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<TipoTransaccion> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpDelete]
        public Respuesta<TipoTransaccion> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosTipoTransaccion.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<TipoTransaccion> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpPost]
        public Respuesta<TipoTransaccion> Insertar(TipoTransaccion model)
        {
            try
            {
                var respuesta = _datosTipoTransaccion.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<TipoTransaccion> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
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