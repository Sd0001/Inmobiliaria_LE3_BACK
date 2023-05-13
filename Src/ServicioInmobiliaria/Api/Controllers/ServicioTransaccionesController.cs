using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioTransaccionesController : ControllerBase, IServicio<Transaccion>
    {
        private readonly IDatos<Transaccion> _datosTransaccion;
        private readonly ILogger<ServicioTransaccionesController> _logger;
        public ServicioTransaccionesController(ILogger<ServicioTransaccionesController> logger,
            IDatos<Transaccion> datosTransaccion)
        {
            _logger = logger;
            _datosTransaccion = datosTransaccion;
        }

        [HttpPut]
        public Respuesta<Transaccion> Actualizar(Transaccion model)
        {
            try
            {
                var respuesta = _datosTransaccion.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<Transaccion> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpDelete]
        public Respuesta<Transaccion> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosTransaccion.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<Transaccion> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpPost]
        public Respuesta<Transaccion> Insertar(Transaccion model)
        {
            try
            {
                var respuesta = _datosTransaccion.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<Transaccion> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpGet]
        public Respuesta<IEnumerable<Transaccion>> Listar()
        {
            try
            {
                var model = _datosTransaccion.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Transaccion>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Transaccion>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}