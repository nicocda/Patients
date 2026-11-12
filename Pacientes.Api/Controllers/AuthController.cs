using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web.Http;

namespace Pacientes.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        [HttpGet, Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(new { ok = true, configurado = ApiContext.Auth.EstaConfigurado });
        }

        [HttpPost, Route("auth/login")]
        public HttpResponseMessage Login([FromBody] LoginRequest req)
        {
            if (!ApiContext.Auth.EstaConfigurado)
                return Request.CreateResponse(HttpStatusCode.ServiceUnavailable,
                    new { error = "El acceso aun no fue configurado en el equipo (ejecutar con --set-pin)." });

            string token;
            int espera;
            if (ApiContext.Auth.TryLogin(req == null ? null : req.Pin, out token, out espera))
                return Request.CreateResponse(HttpStatusCode.OK,
                    new { token, expira = ApiContext.Auth.ExpiracionToken });

            return Rechazo(espera, "PIN incorrecto");
        }

        // Canjea el codigo del QR por un token (el celular queda autorizado sin escribir el PIN).
        [HttpPost, Route("auth/vincular")]
        public HttpResponseMessage Vincular([FromBody] VincularRequest req)
        {
            string token;
            int espera;
            if (ApiContext.Auth.TryVincular(req == null ? null : req.Codigo, out token, out espera))
                return Request.CreateResponse(HttpStatusCode.OK,
                    new { token, expira = ApiContext.Auth.ExpiracionToken });

            return Rechazo(espera, "El codigo no es valido o ya vencio. Genere otro en la PC.");
        }

        // Solo desde la propia PC del servidor: genera el codigo y las direcciones para armar el QR.
        [HttpGet, Route("vincular/nuevo")]
        public HttpResponseMessage NuevoVinculo()
        {
            if (!EsLocal())
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    new { error = "Esta pantalla solo puede abrirse desde la PC del consultorio." });
            if (!ApiContext.Auth.EstaConfigurado)
                return Request.CreateResponse(HttpStatusCode.ServiceUnavailable,
                    new { error = "Falta configurar el PIN (--set-pin)." });

            DateTime expira;
            var codigo = ApiContext.Auth.NuevoCodigoVinculo(out expira);
            int puerto = Request.RequestUri.Port;
            var direcciones = DireccionesLan().Select(ip => "http://" + ip + ":" + puerto).ToArray();
            int segundos = (int)Math.Max(0, (expira - DateTime.UtcNow).TotalSeconds);
            return Request.CreateResponse(HttpStatusCode.OK, new { codigo, segundos, direcciones });
        }

        HttpResponseMessage Rechazo(int espera, string mensaje)
        {
            if (espera > 0)
            {
                var r = Request.CreateResponse((HttpStatusCode)429,
                    new { error = "Demasiados intentos. Espere " + espera + " segundos.", espera });
                r.Headers.Add("Retry-After", espera.ToString());
                return r;
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized, new { error = mensaje });
        }

        bool EsLocal()
        {
            var ctx = Request.GetOwinContext();
            IPAddress ip;
            return ctx != null && IPAddress.TryParse(ctx.Request.RemoteIpAddress, out ip) && IPAddress.IsLoopback(ip);
        }

        static string[] DireccionesLan()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up &&
                            n.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                            n.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .SelectMany(n => n.GetIPProperties().UnicastAddresses)
                .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(a => a.Address.ToString())
                .Where(s => !s.StartsWith("169.254."))   // sin DHCP
                .Distinct()
                .ToArray();
        }
    }
}
