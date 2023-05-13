using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioVisitasController : ControllerBase, IServicio<Visita>
    {
        private readonly IDatos<Visita> _datosVisita;
        private readonly ILogger<ServicioVisitasController> _logger;
        public ServicioVisitasController(ILogger<ServicioVisitasController> logger,
            IDatos<Visita> datosVisita)
        {
            _logger = logger;
            _datosVisita = datosVisita;
        }

        [HttpPut]
        public Respuesta<Visita> Actualizar(Visita model)
        {
            try
            {
                var respuesta = _datosVisita.Actualizar(model);//TODO consumir datos
                if (respuesta.Completa)
                    this.Response.StatusCode = (int)HttpStatusCode.OK;
                else
                    this.Response.StatusCode = (int)HttpStatusCode.NotModified;
                return respuesta;
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<Visita> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpDelete]
        public Respuesta<Visita> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosVisita.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<Visita> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpPost]
        public Respuesta<Visita> Insertar(Visita model)
        {
            try
            {
                var respuesta = _datosVisita.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<Visita> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }

        [HttpGet]
        public Respuesta<IEnumerable<Visita>> Listar()
        {
            try
            {
                var model = _datosVisita.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Visita>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Visita>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}