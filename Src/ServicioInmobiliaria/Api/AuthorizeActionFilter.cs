using System.Security.Claims;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Api
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly IDatos<Persona> _datosPersona;

        public AuthorizeActionFilter(IDatos<Persona> datosEstado)
        {
            _datosPersona = datosEstado;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                StringValues authorization;

                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out authorization))
                {
                    var usuario = authorization;

                    var contrasena = "";
                    var conectado = _datosPersona.Obtener(x => x.Email == usuario.Value && x.Password == contrasena).Any();//TODO consumir datos
                    if (!conectado)
                    {
                        var respueta = new Respuesta<bool?>
                        {
                            Completa = false,
                            Mensaje = "Token Validation Has Failed. Request Access Denied",
                            Datos = null
                        };
                        context.Result = new JsonResult(respueta)
                        { StatusCode = StatusCodes.Status401Unauthorized };

                    }
                }
            catch (Exception ex)
            {

            }
        }
    }
}