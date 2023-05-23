using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
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

        /// <summary>
        /// Actualiza los datos de una transacción en la aplicación.
        /// </summary>
        /// <param name="model">Datos de la transacción a actualizar.</param>
        /// <returns>Respuesta con el resultado de la actualización.</returns>
        /// <response code="200">Los datos de la transacción se actualizaron correctamente.</response>
        /// <response code="304">No se realizaron cambios en los datos de la transacción.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Elimina una transacción de la aplicación.
        /// </summary>
        /// <param name="id">ID de la transacción a eliminar.</param>
        /// <returns>Respuesta con el resultado de la eliminación.</returns>
        /// <response code="200">La transacción se eliminó correctamente.</response>
        /// <response code="404">La transacción con el ID especificado no fue encontrada.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Inserta una nueva transacción en la aplicación.
        /// </summary>
        /// <param name="model">Datos de la transacción a insertar.</param>
        /// <returns>Respuesta con el resultado de la inserción.</returns>
        /// <response code="200">La transacción se insertó correctamente.</response>
        /// <response code="304">La transacción no se pudo insertar debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<Transaccion>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Obtiene la lista de transacciones de la aplicación.
        /// </summary>
        /// <returns>Respuesta con la lista de transacciones.</returns>
        /// <response code="200">Lista de transacciones obtenida correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<Transaccion>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<Transaccion>>), (int)HttpStatusCode.InternalServerError)]
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