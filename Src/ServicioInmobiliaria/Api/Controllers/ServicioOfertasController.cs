using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inmobiliaria.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioOfertasController : ControllerBase, IServicioOfertas
    {
        private readonly IDatos<Oferta> _datosOferta;
        private readonly ILogger<ServicioOfertasController> _logger;
        public ServicioOfertasController(ILogger<ServicioOfertasController> logger,
            IDatos<Oferta> datosOferta)
        {
            _logger = logger;
            _datosOferta = datosOferta;
        }
        /// <summary>
        /// Método para actualizar una oferta
        /// </summary>
        /// <param name="model">Datos de la oferta a actualizar.</param>
        /// <response code="200">La oferta se actualizó correctamente.</response>
        /// <response code="304">la oferta no se pudo actualizar debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se actualiza una oferta <br/>
        /// </remarks>

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Oferta> Actualizar(Oferta model)
        {
            try
            {
                var respuesta = _datosOferta.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<Oferta> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para eliminar una oferta
        /// </summary>
        /// <param name="id">ID del tipo de oferta a eliminar.</param>
        /// <returns>Respuesta con los resultados de la eliminación.</returns>
        /// <response code="200">la oferta se eliminó correctamente.</response>
        /// <response code="404">la oferta no se encontró.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se elimina una oferta <br/>
        /// </remarks>

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Oferta> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosOferta.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<Oferta> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para crear una oferta
        /// </summary>
        /// <param name="model">Datos de la oferta a insertar.</param>
        /// <response code="200">La oferta se creo correctamente.</response>
        /// <response code="304">la oferta no se pudo crear debido a datos no modificados.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se crea una oferta <br/>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified, Type = typeof(Respuesta<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<Oferta> Insertar(Oferta model)
        {
            try
            {
                var respuesta = _datosOferta.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<Oferta> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Método para consultar una oferta
        /// </summary>
        /// <response code="200">La oferta se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se consulta una oferta <br/>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<IEnumerable<Oferta>> Listar()
        {
            try
            {
                var model = _datosOferta.Obtener(x=>x.IdEstado==1);//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Oferta>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Oferta>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
         /// <summary>
        /// Método para consultar las ofertas activas
        /// </summary>
        /// <response code="200">Listar la oferta se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        /// <remarks>
        /// Con este método se consulta si una oferta se encuentra activa o no <br/>
        /// 1 => 'Activo' <br/>
        /// 2 => 'Eliminado'
        /// </remarks>
        [HttpGet]
        [Route("api/[controller]/Activas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<>))]
        public Respuesta<IEnumerable<Oferta>> ListarActivas()
        {
            try
            {
                var model = _datosOferta.Obtener(x => x.Transacciones.Any() != true && x.IdEstado==1);//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Oferta>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Oferta>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}