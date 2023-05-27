using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioInmueblesController : ControllerBase, IServicio<Inmueble>
    {
        private readonly IDatos<Inmueble> _datosInmueble;
        private readonly ILogger<ServicioInmueblesController> _logger;
        public ServicioInmueblesController(ILogger<ServicioInmueblesController> logger,
            IDatos<Inmueble> datosInmueble)
        {
            _logger = logger;
            _datosInmueble = datosInmueble;
        }
        /// <summary>
        /// Método para actualizar un inmueble
        /// </summary>
        /// <param name="model">Datos del inmueble a actualizar.</param>
        /// <response code="200">El inmueble se actualizó correctamente.</response>
        /// <response code="304">El inmueble no se pudo actualizar debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se actualiza un inmueble <br/>
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Inmueble> Actualizar(Inmueble model)
        {
            try
            {
                var respuesta = _datosInmueble.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<Inmueble> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para eliminar un inmueble
        /// </summary>
        /// <param name="id">ID del tipo inmueble a eliminar.</param>
        /// <returns>Respuesta con los resultados de la eliminación.</returns>
        /// <response code="200">El inmueble que se elimin� correctamente.</response>
        /// <response code="404">El inmueble no se encontr�.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se elimina un inmueble <br/>
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        
        public Respuesta<Inmueble> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosInmueble.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<Inmueble> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para crear un inmueble
        /// </summary>
        /// <param name="model">Datos del inmueble a insertar.</param>
        /// <response code="200">El inmueble se creo correctamente.</response>
        /// <response code="304">El inmueble no se pudo crear debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se crea un inmueble <br/>
        /// </remarks>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Inmueble> Insertar(Inmueble model)
        {
            try
            {
                var respuesta = _datosInmueble.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<Inmueble> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para consultar un inmueble
        /// </summary>
        /// <response code="200">El inmueble se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se consulta un inmueble <br/>
        /// </remarks>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<IEnumerable<Inmueble>> Listar()
        {
            try
            {
                var model = _datosInmueble.Obtener(x=>x.IdEstado==1);//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Inmueble>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Inmueble>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}