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
        
        
        /// <summary>
        /// Actualiza los datos de un tipo de inmueble en la aplicación.
        /// </summary>
        /// <param name="model">Objeto que contiene los datos actualizados del tipo de inmueble.</param>
        /// <returns>Respuesta con los resultados de la actualización.</returns>
        /// <response code="200">El tipo de inmueble se actualizó correctamente.</response>
        /// <response code="304">No se realizaron cambios en el tipo de inmueble.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Respuesta<TipoInmueble>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<TipoInmueble>), (int)HttpStatusCode.InternalServerError)]

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

        /// <summary>
        /// Elimina un tipo de inmueble de la aplicación.
        /// </summary>
        /// <param name="id">ID del tipo de inmueble a eliminar.</param>
        /// <returns>Respuesta con los resultados de la eliminación.</returns>
        /// <response code="200">El tipo de inmueble se eliminó correctamente.</response>
        /// <response code="404">El tipo de inmueble no se encontró.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Respuesta<TipoInmueble>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Respuesta<TipoInmueble>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Inserta un nuevo tipo de inmueble en la aplicación.
        /// </summary>
        /// <param name="model">Objeto que contiene los datos del tipo de inmueble a insertar.</param>
        /// <returns>Respuesta con los resultados de la inserción.</returns>
        /// <response code="200">El tipo de inmueble se insertó correctamente.</response>
        /// <response code="304">No se realizaron cambios en el tipo de inmueble.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Respuesta<TipoInmueble>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<TipoInmueble>), (int)HttpStatusCode.InternalServerError)]
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

        /// <summary>
        /// Obtiene la lista de tipos de inmuebles de la aplicación.
        /// </summary>
        /// <returns>Respuesta con la lista de tipos de inmuebles.</returns>
        /// <response code="200">La lista de tipos de inmuebles se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<TipoInmueble>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<TipoInmueble>>), (int)HttpStatusCode.InternalServerError)]
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