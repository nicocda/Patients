using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace Pacientes
{
    static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("settings.json",
                         optional: true,
                         reloadOnChange: true);
            Configuration = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Principal());
            Application.Run(new ListarSesiones());

            
        }
    }
}
