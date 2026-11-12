using System;
using Pacientes.Api.Security;
using Server;

namespace Pacientes.Api
{
    /// <summary>
    /// Configuracion compartida del modulo. El host la inicializa una sola vez al arrancar.
    /// </summary>
    public static class ApiContext
    {
        static readonly object _lock = new object();

        public static string DatabasePath { get; private set; }
        public static AuthService Auth { get; private set; }

        public static void Initialize(string databasePath, AuthService auth)
        {
            DatabasePath = databasePath;
            Auth = auth;
            // La API nunca crea ni modifica el esquema: usa la base tal como esta.
            System.Data.Entity.Database.SetInitializer<DatabaseContext>(null);
        }

        /// <summary>
        /// SQLite admite un solo escritor: se serializa todo el acceso a datos y se usa
        /// una instancia nueva de Logica (y de contexto EF) por operacion.
        /// </summary>
        public static T Run<T>(Func<Logica, T> accion)
        {
            lock (_lock)
                return accion(new Logica(DatabasePath));
        }

        public static void Run(Action<Logica> accion)
        {
            Run<object>(l => { accion(l); return null; });
        }
    }
}
