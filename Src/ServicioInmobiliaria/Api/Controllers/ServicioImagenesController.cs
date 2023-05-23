using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioImagenesController : ControllerBase, IServicio<Imagen>
    {
        private readonly IDatos<Imagen> _datosImagen;
        private readonly ILogger<ServicioImagenesController> _logger;
        public ServicioImagenesController(ILogger<ServicioImagenesController> logger,
            IDatos<Imagen> datosImagen)
        {
            _logger = logger;
            _datosImagen = datosImagen;
        }

        /// <summary>
        /// Método  para actualizar una imagen de una oferta
        /// </summary>
        /// <param name="model">Datos de la imagen a actualizar.</param>
        /// <response code="200">La imagen se actualizó correctamente.</response>
        /// <response code="304">la imagen no se pudo actualizar debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método  se actualiza la imagen que se tiene publicada para una oferta <br/>
        /// </remarks>

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Imagen> Actualizar(Imagen model)
        {
            try
            {
                var respuesta = _datosImagen.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<Imagen> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para eliminar una imagen de una oferta
        /// </summary>
        /// <param name="id">ID del tipo de imagen a eliminar.</param>
        /// <returns>Respuesta con los resultados de la eliminación.</returns>
        /// <response code="200">La imagen se elimin� correctamente.</response>
        /// <response code="404">La imagen no se encontr�.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se elimina la imagen que se tiene publicada para una oferta <br/>
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Imagen> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosImagen.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<Imagen> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para crear una imagen de una oferta
        /// </summary>
        /// <param name="model">Datos de la imagen a insertar.</param>
        /// <response code="200">La imagen se inserto correctamente.</response>
        /// <response code="304">la imagen no se pudo insertar debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se crea la imagen que se tiene publicada para una oferta <br/>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Imagen> Insertar(Imagen model)
        {
            try
            {
                var respuesta = _datosImagen.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<Imagen> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para consultar una imagen de una oferta
        /// </summary>
        /// <response code="200">La imagen se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se consulta la imagen que se tiene publicada para una oferta <br/>
        /// </remarks>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<IEnumerable<Imagen>> Listar()
        {
            try
            {
                var model = _datosImagen.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Imagen>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Imagen>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}