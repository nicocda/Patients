using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Owin.Hosting;
using Pacientes.Api;
using Pacientes.Api.Security;

namespace Pacientes.Web
{
    static class Program
    {
        static int Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var auth = new AuthService(Path.Combine(baseDir, "auth.dat"));

            if (args.Contains("--set-pin"))
                return EstablecerPin(auth);

            var dbPath = ConfigurationManager.AppSettings["DatabasePath"] ?? "dbPacientes.db";
            if (!Path.IsPathRooted(dbPath))
                dbPath = Path.Combine(baseDir, dbPath);

            // Opciones para desarrollo: base y carpeta web alternativas (no se usan en la instalacion normal).
            foreach (var a in args.Where(x => x.StartsWith("--db=")))
                dbPath = Path.GetFullPath(a.Substring("--db=".Length));
            foreach (var a in args.Where(x => x.StartsWith("--wwwroot=")))
                Startup.RaizWeb = Path.GetFullPath(a.Substring("--wwwroot=".Length));

            var respaldo = args.FirstOrDefault(a => a == "--backup" || a.StartsWith("--backup="));
            if (respaldo != null)
            {
                // Sin ruta explicita: siempre "backups" junto al programa, y ademas BackupDir si esta configurada.
                var destinos = new System.Collections.Generic.List<string>();
                if (respaldo.Contains("="))
                    destinos.Add(respaldo.Substring(respaldo.IndexOf('=') + 1));
                else
                {
                    destinos.Add("backups");
                    var extra = ConfigurationManager.AppSettings["BackupDir"];
                    if (!string.IsNullOrWhiteSpace(extra))
                        destinos.Add(extra);
                }
                return Respaldar(dbPath, destinos.Select(d => Path.IsPathRooted(d) ? d : Path.Combine(baseDir, d)).ToList());
            }

            var url = ConfigurationManager.AppSettings["Url"] ?? "http://+:8080/";
            foreach (var a in args.Where(x => x.StartsWith("--url=")))
                url = a.Substring("--url=".Length);

            if (!File.Exists(dbPath))
            {
                Console.Error.WriteLine("No se encontro la base de datos: " + dbPath);
                Console.Error.WriteLine("Indique su ubicacion en 'DatabasePath' (Pacientes.Web.exe.config).");
                return 2;
            }
            if (!auth.EstaConfigurado)
            {
                Console.Error.WriteLine("Falta configurar el PIN de acceso. Ejecute:  Pacientes.Web.exe --set-pin");
                return 3;
            }

            ActivarWal(dbPath);
            ApiContext.Initialize(dbPath, auth);

            try
            {
                using (WebApp.Start<Startup>(url))
                {
                    Console.WriteLine("Pacientes en linea: " + url.Replace("+", "<este-equipo>"));
                    Console.WriteLine("Base de datos:      " + dbPath);
                    Console.WriteLine("Ctrl+C para detener.");
                    var salir = new ManualResetEvent(false);
                    Console.CancelKeyPress += (s, e) => { e.Cancel = true; salir.Set(); };
                    salir.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("No se pudo iniciar el servidor: " + (ex.InnerMost().Message));
                Console.Error.WriteLine("Si es un error de acceso denegado, reserve la direccion con instalar.bat.");
                return 4;
            }
            return 0;
        }

        /// <summary>
        /// Copia consistente de la base (VACUUM INTO: segura aunque el servidor este escribiendo).
        /// Conserva las ultimas 30 copias. Las copias contienen datos de salud sin cifrar.
        /// </summary>
        static int Respaldar(string dbPath, System.Collections.Generic.List<string> carpetas)
        {
            if (!File.Exists(dbPath))
            {
                Console.Error.WriteLine("No se encontro la base de datos: " + dbPath);
                return 2;
            }
            var nombre = "dbPacientes-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".db";
            string primera = null;
            int fallos = 0;
            foreach (var carpeta in carpetas)
            {
                // Un destino no disponible (disco externo desconectado) no debe impedir los demas.
                try
                {
                    Directory.CreateDirectory(carpeta);
                    var destino = Path.Combine(carpeta, nombre);
                    if (primera == null)
                    {
                        using (var cn = new System.Data.SQLite.SQLiteConnection("Data Source=" + dbPath))
                        {
                            cn.Open();
                            using (var cmd = cn.CreateCommand())
                            {
                                cmd.CommandText = "VACUUM INTO '" + destino.Replace("'", "''") + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                        primera = destino;
                    }
                    else
                        File.Copy(primera, destino, true);

                    foreach (var vieja in Directory.GetFiles(carpeta, "dbPacientes-*.db").OrderByDescending(f => f).Skip(30))
                        File.Delete(vieja);
                    Console.WriteLine("Copia creada: " + destino);
                }
                catch (Exception ex)
                {
                    fallos++;
                    Console.Error.WriteLine("No se pudo copiar a '" + carpeta + "': " + ex.Message);
                }
            }
            return fallos == carpetas.Count ? 1 : 0;
        }

        static int EstablecerPin(AuthService auth)
        {
            Console.Write("Nuevo PIN (min. 6 caracteres): ");
            var pin = LeerOculto();
            Console.Write("Repita el PIN: ");
            var pin2 = LeerOculto();
            if (pin != pin2)
            {
                Console.Error.WriteLine("Los PIN no coinciden.");
                return 1;
            }
            try
            {
                auth.EstablecerPin(pin);
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }
            Console.WriteLine("PIN guardado (cifrado). Las sesiones abiertas deberan iniciar de nuevo.");
            return 0;
        }

        static string LeerOculto()
        {
            if (Console.IsInputRedirected)
                return Console.ReadLine();
            var sb = new System.Text.StringBuilder();
            ConsoleKeyInfo k;
            while ((k = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (k.Key == ConsoleKey.Backspace && sb.Length > 0)
                    sb.Length--;
                else if (!char.IsControl(k.KeyChar))
                    sb.Append(k.KeyChar);
            }
            Console.WriteLine();
            return sb.ToString();
        }

        /// <summary>
        /// WAL permite que la WinForms (si sigue abierta) y la API lean/escriban el mismo .db sin bloquearse.
        /// El modo queda guardado en el archivo.
        /// </summary>
        static void ActivarWal(string dbPath)
        {
            using (var cn = new System.Data.SQLite.SQLiteConnection("Data Source=" + dbPath))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "PRAGMA journal_mode=WAL;";
                    cmd.ExecuteScalar();
                }
            }
        }

        static Exception InnerMost(this Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
