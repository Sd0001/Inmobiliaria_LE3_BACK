using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Inmobiliaria.Api.Modules
{
    public class AuthorizeActionFilter : ActionFilterAttribute,   IAuthorizationFilter
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
                var respueta = new Respuesta<bool?>
                {
                    Completa = false,
                    Mensaje = "Token Validation Has Failed. Request Access Denied",
                    Datos = null
                };
                var resultUnautorized = new JsonResult(respueta)
                { StatusCode = StatusCodes.Status401Unauthorized };

                if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out authorization))
                {
                    context.Result = resultUnautorized;
                    return;
                }
                if ( !authorization.ToString().StartsWith("Basic"))
                {
                    context.Result = resultUnautorized;
                    return;
                }
                string encodedUsernamePassword = authorization.ToString().Substring("Basic ".Length).Trim();
                //the coding should be iso or you could use ASCII and UTF-8 decoder
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                var userParam =  usernamePassword.Split(':');
                
                var conectado = _datosPersona.Obtener(x => x.Email == userParam[0] && x.Password == userParam[1]).Any();//TODO consumir datos
                if (!conectado)
                {
                    context.Result = resultUnautorized;
                }

            }
            catch (Exception ex)
            {
                var respueta = new Respuesta<bool?>
                {
                    Completa = false,
                    Mensaje = ex.Message,
                    Datos = null
                };
                context.Result = new JsonResult(respueta)
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}