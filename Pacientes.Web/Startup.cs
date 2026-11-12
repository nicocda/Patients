using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;
using Pacientes.Api;

namespace Pacientes.Web
{
    public class Startup
    {
        /// <summary>Carpeta de archivos web. Null = "wwwroot" junto al exe (en desarrollo se apunta al repo).</summary>
        public static string RaizWeb { get; set; }

        public void Configuration(IAppBuilder app)
        {
            // Datos de salud: nada de la API debe quedar en caches del navegador o de proxies.
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
                ctx.Response.Headers["Referrer-Policy"] = "no-referrer";
                ctx.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
                if (ctx.Request.Path.StartsWithSegments(new PathString("/api")))
                    ctx.Response.Headers["Cache-Control"] = "no-store";
                await next();
            });

            app.Map("/api", api => ApiStartup.Configure(api));

            var raiz = RaizWeb ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
            var tipos = new FileExtensionContentTypeProvider();
            tipos.Mappings[".webmanifest"] = "application/manifest+json";
            app.UseFileServer(new FileServerOptions
            {
                FileSystem = new PhysicalFileSystem(raiz),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                StaticFileOptions =
                {
                    ContentTypeProvider = tipos,
                    // Revalidar siempre (ETag): asi un celular nunca queda con una version vieja tras actualizar.
                    OnPrepareResponse = c => c.OwinContext.Response.Headers["Cache-Control"] = "no-cache"
                }
            });
        }
    }
}
