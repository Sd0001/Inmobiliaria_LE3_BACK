using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
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

        /// <summary>
        /// Actualiza los datos de una visita en la aplicación.
        /// </summary>
        /// <param name="model">Datos de la visita a actualizar.</param>
        /// <returns>Respuesta con el resultado de la operación.</returns>
        /// <response code="200">Los datos de la visita se actualizaron correctamente.</response>
        /// <response code="304">No se realizaron cambios en los datos de la visita.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Elimina una visita de la aplicación.
        /// </summary>
        /// <param name="id">ID de la visita a eliminar.</param>
        /// <returns>Respuesta con el resultado de la operación.</returns>
        /// <response code="200">La visita se eliminó correctamente.</response>
        /// <response code="404">La visita no fue encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Inserta una nueva visita en la aplicación.
        /// </summary>
        /// <param name="model">Datos de la visita a insertar.</param>
        /// <returns>Respuesta con el resultado de la operación.</returns>
        /// <response code="200">La visita se insertó correctamente.</response>
        /// <response code="304">No se modificó ningún recurso existente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<Visita>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Obtiene la lista de visitas de la aplicación.
        /// </summary>
        /// <returns>Respuesta con la lista de visitas.</returns>
        /// <response code="200">La operación se completó correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<Visita>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<Visita>>), (int)HttpStatusCode.InternalServerError)]
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