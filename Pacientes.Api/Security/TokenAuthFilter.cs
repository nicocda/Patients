using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Pacientes.Api.Security
{
    /// <summary>
    /// Filtro global: toda la API exige "Authorization: Bearer {token}" salvo [AllowAnonymous].
    /// </summary>
    public class TokenAuthFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext ctx)
        {
            if (ctx.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                ctx.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                return;

            var h = ctx.Request.Headers.Authorization;
            if (h != null && h.Scheme == "Bearer" && ApiContext.Auth.ValidarToken(h.Parameter))
                return;

            ctx.Response = ctx.Request.CreateResponse(HttpStatusCode.Unauthorized, new { error = "No autorizado" });
        }
    }
}
