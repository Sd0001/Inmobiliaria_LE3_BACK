using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
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
                var model = _datosOferta.Obtener();//TODO consumir datos
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
                var model = _datosOferta.Obtener(x => !x.Transacciones.Any());//TODO consumir datos
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