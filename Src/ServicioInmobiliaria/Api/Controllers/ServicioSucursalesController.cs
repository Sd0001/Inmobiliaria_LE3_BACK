using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioSucursalesController : ControllerBase, IServicio<Sucursal>
    {
        private readonly IDatos<Sucursal> _datosSucursal;
        private readonly ILogger<ServicioSucursalesController> _logger;
        public ServicioSucursalesController(ILogger<ServicioSucursalesController> logger,
            IDatos<Sucursal> datosSucursal)
        {
            _logger = logger;
            _datosSucursal = datosSucursal;
        }
        /// <summary>
        /// Actualiza los datos de una sucursal en la aplicación.
        /// </summary>
        /// <param name="model">Objeto que contiene los datos actualizados de la sucursal.</param>
        /// <returns>Respuesta con los resultados de la actualización.</returns>
        /// <response code="200">La sucursal se actualizó correctamente.</response>
        /// <response code="304">No se realizaron cambios en la sucursal.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Respuesta<Sucursal>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<Sucursal>), (int)HttpStatusCode.InternalServerError)]
        public Respuesta<Sucursal> Actualizar(Sucursal model)
        {
            try
            {
                var respuesta = _datosSucursal.Actualizar(model);//TODO consumir datos
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
                return new Respuesta<Sucursal> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Elimina una sucursal de la aplicación.
        /// </summary>
        /// <param name="id">ID de la sucursal a eliminar.</param>
        /// <returns>Respuesta con los resultados de la eliminación.</returns>
        /// <response code="200">La sucursal se eliminó correctamente.</response>
        /// <response code="404">La sucursal no se encontró.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Respuesta<Sucursal>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Respuesta<Sucursal>), (int)HttpStatusCode.InternalServerError)]

        public Respuesta<Sucursal> Eliminar(int id)
        {
            try
            {
                var respuesta = _datosSucursal.Eliminar(id);//TODO consumir datos
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
                return new Respuesta<Sucursal> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
        /// <summary>
        /// Inserta una nueva sucursal en la aplicación.
        /// </summary>
        /// <param name="model">Objeto que contiene los datos de la sucursal a insertar.</param>
        /// <returns>Respuesta con los resultados de la inserción.</returns>
        /// <response code="200">La sucursal se insertó correctamente.</response>
        /// <response code="304">No se realizaron cambios en la sucursal.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Respuesta<Sucursal>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        [ProducesResponseType(typeof(Respuesta<Sucursal>), (int)HttpStatusCode.InternalServerError)]
        public Respuesta<Sucursal> Insertar(Sucursal model)
        {
            try
            {
                var respuesta = _datosSucursal.Insertar(model);//TODO consumir datos              
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
                return new Respuesta<Sucursal> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }


        /// <summary>
        /// Obtiene la lista de sucursales de la aplicación.
        /// </summary>
        /// <returns>Respuesta con la lista de sucursales.</returns>
        /// <response code="200">La lista de sucursales se obtuvo correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<Sucursal>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Respuesta<IEnumerable<Sucursal>>), (int)HttpStatusCode.InternalServerError)]
        public Respuesta<IEnumerable<Sucursal>> Listar()
        {
            try
            {
                var model = _datosSucursal.Obtener();//TODO consumir datos
                this.Response.StatusCode = (int)HttpStatusCode.OK;

                return new Respuesta<IEnumerable<Sucursal>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new Respuesta<IEnumerable<Sucursal>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}