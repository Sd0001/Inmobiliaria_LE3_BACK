using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioTipoInmueblesController : ControllerBase, IServicio<TipoInmueble>
    {
        private readonly IDatos<TipoInmueble> _datosTipoInmueble;
        private readonly ILogger<ServicioTipoInmueblesController> _logger;
        public ServicioTipoInmueblesController(ILogger<ServicioTipoInmueblesController> logger,
            IDatos<TipoInmueble> datosTipoInmueble)
        {
            _logger = logger;
            _datosTipoInmueble = datosTipoInmueble;
        }

        [HttpPut]
        public Respuesta<TipoInmueble> Actualizar(TipoInmueble model)
        {
            try
            {
                var respuesta = _datosTipoInmueble.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<TipoInmueble> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpDelete]
        public Respuesta<TipoInmueble> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosTipoInmueble.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<TipoInmueble> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpPost]
        public Respuesta<TipoInmueble> Insertar(TipoInmueble model)
        {
            try
            {
                var respuesta = _datosTipoInmueble.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<TipoInmueble> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpGet]
        public Respuesta<IEnumerable<TipoInmueble>> Listar()
        {
            try
            {
                var model = _datosTipoInmueble.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<TipoInmueble>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<TipoInmueble>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}