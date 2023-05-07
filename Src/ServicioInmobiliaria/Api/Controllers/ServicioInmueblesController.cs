using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicioInmueblesController : ControllerBase, IServicioInmuebles
    {       
        private readonly ILogger<ServicioInmueblesController> _logger;
        private readonly IDatos<Oferta> _datosOferta;

        public ServicioInmueblesController(ILogger<ServicioInmueblesController> logger,
            IDatos<Oferta> datosOferta)
        {
            _logger = logger;
            _datosOferta = datosOferta;
        }

        [HttpGet]
        public Respuesta<IEnumerable<Oferta>> ListarInmueblesOfertados()
        {
            try
            {
                var model = _datosOferta.Obtener(x => !x.Transacciones.Any());//TODO consumir datos
                return new Respuesta<IEnumerable<Oferta>> { Completa = true, Mensaje = "", Datos = model };
            }
            catch (Exception ex)
            {

                return new Respuesta<IEnumerable<Oferta>> { Completa = false, Mensaje = ex.Message, Datos = null };
            }
        }
    }
}