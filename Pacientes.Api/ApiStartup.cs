using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Pacientes.Api.Security;

namespace Pacientes.Api
{
    public static class ApiStartup
    {
        /// <summary>
        /// Registra la API en el pipeline OWIN. El host la monta bajo "/api".
        /// </summary>
        public static void Configure(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            var json = new JsonMediaTypeFormatter();
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
            json.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
            config.Formatters.Add(json);

            config.Filters.Add(new TokenAuthFilter());
            config.Filters.Add(new ErrorFilter());

            app.UseWebApi(config);
        }

        /// <summary>
        /// Las reglas de negocio de Logica lanzan Exception "pelada": se devuelven como 400/404
        /// con su mensaje. Cualquier otro error es inesperado: 500 sin detalles hacia afuera.
        /// </summary>
        class ErrorFilter : ExceptionFilterAttribute
        {
            public override void OnException(HttpActionExecutedContext ctx)
            {
                var ex = ctx.Exception;
                var req = ctx.Request;

                if (ex.GetType() == typeof(Exception))
                {
                    var status = ex.Message.IndexOf("no encontrad", StringComparison.OrdinalIgnoreCase) >= 0
                        ? HttpStatusCode.NotFound
                        : HttpStatusCode.BadRequest;
                    ctx.Response = req.CreateResponse(status, new { error = ex.Message });
                    return;
                }

                Console.Error.WriteLine("[{0:s}] {1}", DateTime.Now, ex);
                ctx.Response = req.CreateResponse(HttpStatusCode.InternalServerError,
                    new { error = "Error interno del servidor" });
            }
        }
    }
}
