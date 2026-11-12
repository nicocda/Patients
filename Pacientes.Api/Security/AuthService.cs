using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Pacientes.Api.Security
{
    /// <summary>
    /// PIN/clave de acceso + tokens firmados. Nada se guarda en texto plano:
    /// el archivo de credenciales contiene solo el hash PBKDF2 del PIN y la clave de firma,
    /// y esta cifrado con DPAPI (ligado a esta maquina).
    /// </summary>
    public class AuthService
    {
        const int Iteraciones = 150000;
        const int MinLongitudPin = 6;
        static readonly byte[] Entropia = Encoding.UTF8.GetBytes("Pacientes.Api.auth.v1");
        static readonly TimeSpan VidaToken = TimeSpan.FromDays(180);
        static readonly TimeSpan VidaCodigoVinculo = TimeSpan.FromMinutes(10);
        const string AlfabetoCodigo = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // sin caracteres ambiguos (0/O, 1/I)

        class Credenciales
        {
            public byte[] Sal { get; set; }
            public byte[] Hash { get; set; }
            public int Iteraciones { get; set; }
            public byte[] ClaveFirma { get; set; }
        }

        readonly string _ruta;
        readonly object _lock = new object();
        Credenciales _cred;
        int _fallos;
        DateTime _bloqueadoHasta = DateTime.MinValue;

        public AuthService(string rutaArchivo)
        {
            _ruta = rutaArchivo;
            Cargar();
        }

        public bool EstaConfigurado { get { return _cred != null; } }

        public void EstablecerPin(string pin)
        {
            if (pin == null || pin.Length < MinLongitudPin)
                throw new ArgumentException("El PIN debe tener al menos " + MinLongitudPin + " caracteres.");

            var sal = Aleatorio(16);
            var cred = new Credenciales
            {
                Sal = sal,
                Iteraciones = Iteraciones,
                Hash = Derivar(pin, sal, Iteraciones),
                ClaveFirma = Aleatorio(32)   // cambiar el PIN invalida los tokens anteriores
            };

            var json = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cred));
            var protegido = ProtectedData.Protect(json, Entropia, DataProtectionScope.LocalMachine);
            File.WriteAllBytes(_ruta, protegido);
            lock (_lock)
            {
                _cred = cred;
                _fallos = 0;
                _bloqueadoHasta = DateTime.MinValue;
            }
        }

        /// <summary>
        /// Devuelve un token si el PIN es correcto. Tras 5 fallos seguidos bloquea los intentos
        /// de forma creciente (frena la fuerza bruta desde la red local).
        /// </summary>
        public bool TryLogin(string pin, out string token, out int segundosEspera)
        {
            token = null;
            segundosEspera = 0;
            Credenciales cred;
            lock (_lock)
            {
                cred = _cred;
                if (cred == null)
                    return false;
                var ahora = DateTime.UtcNow;
                if (_bloqueadoHasta > ahora)
                {
                    segundosEspera = (int)Math.Ceiling((_bloqueadoHasta - ahora).TotalSeconds);
                    return false;
                }
            }

            bool ok = pin != null && pin.Length <= 128 &&
                      IgualesTiempoConstante(Derivar(pin, cred.Sal, cred.Iteraciones), cred.Hash);

            lock (_lock)
            {
                if (ok)
                {
                    _fallos = 0;
                    token = EmitirToken(cred);
                    return true;
                }
                _fallos++;
                if (_fallos >= 5)
                {
                    var espera = Math.Min(900, 15 * (1 << Math.Min(_fallos - 5, 6)));
                    _bloqueadoHasta = DateTime.UtcNow.AddSeconds(espera);
                    segundosEspera = espera;
                }
            }
            return false;
        }

        string _codigoVinculo;
        DateTime _codigoVinculoExpira = DateTime.MinValue;

        /// <summary>
        /// Codigo de un solo uso (10 min) para autorizar un celular escaneando un QR,
        /// sin tener que escribir el PIN en el telefono.
        /// </summary>
        public string NuevoCodigoVinculo(out DateTime expira)
        {
            var bytes = Aleatorio(10);
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(AlfabetoCodigo[b % AlfabetoCodigo.Length]);
            lock (_lock)
            {
                _codigoVinculo = sb.ToString();
                _codigoVinculoExpira = DateTime.UtcNow.Add(VidaCodigoVinculo);
                expira = _codigoVinculoExpira;
                return _codigoVinculo;
            }
        }

        /// <summary>Canjea el codigo (una sola vez) por un token. Los fallos cuentan para el bloqueo.</summary>
        public bool TryVincular(string codigo, out string token, out int segundosEspera)
        {
            token = null;
            segundosEspera = 0;
            lock (_lock)
            {
                if (_cred == null)
                    return false;
                var ahora = DateTime.UtcNow;
                if (_bloqueadoHasta > ahora)
                {
                    segundosEspera = (int)Math.Ceiling((_bloqueadoHasta - ahora).TotalSeconds);
                    return false;
                }
                bool ok = _codigoVinculo != null && codigo != null && _codigoVinculoExpira > ahora &&
                          IgualesTiempoConstante(Encoding.ASCII.GetBytes(_codigoVinculo),
                                                 Encoding.ASCII.GetBytes(codigo.Trim().ToUpperInvariant()));
                if (ok)
                {
                    _codigoVinculo = null;   // un solo uso
                    _fallos = 0;
                    token = EmitirToken(_cred);
                    return true;
                }
                _fallos++;
                if (_fallos >= 5)
                {
                    var espera = Math.Min(900, 15 * (1 << Math.Min(_fallos - 5, 6)));
                    _bloqueadoHasta = ahora.AddSeconds(espera);
                    segundosEspera = espera;
                }
            }
            return false;
        }

        public bool ValidarToken(string token)
        {
            var cred = _cred;
            if (cred == null || string.IsNullOrEmpty(token))
                return false;
            var partes = token.Split('.');
            if (partes.Length != 2)
                return false;
            try
            {
                var firmaEsperada = Firmar(cred, partes[0]);
                if (!IgualesTiempoConstante(Encoding.ASCII.GetBytes(firmaEsperada), Encoding.ASCII.GetBytes(partes[1])))
                    return false;
                long exp = long.Parse(Encoding.ASCII.GetString(Base64UrlDecodificar(partes[0])));
                return DateTimeOffset.UtcNow.ToUnixTimeSeconds() < exp;
            }
            catch
            {
                return false;
            }
        }

        public DateTime ExpiracionToken { get { return DateTime.UtcNow.Add(VidaToken); } }

        string EmitirToken(Credenciales cred)
        {
            long exp = DateTimeOffset.UtcNow.Add(VidaToken).ToUnixTimeSeconds();
            string payload = Base64UrlCodificar(Encoding.ASCII.GetBytes(exp.ToString()));
            return payload + "." + Firmar(cred, payload);
        }

        static string Firmar(Credenciales cred, string payload)
        {
            using (var hmac = new HMACSHA256(cred.ClaveFirma))
                return Base64UrlCodificar(hmac.ComputeHash(Encoding.ASCII.GetBytes(payload)));
        }

        void Cargar()
        {
            if (!File.Exists(_ruta))
                return;
            var json = ProtectedData.Unprotect(File.ReadAllBytes(_ruta), Entropia, DataProtectionScope.LocalMachine);
            _cred = JsonConvert.DeserializeObject<Credenciales>(Encoding.UTF8.GetString(json));
        }

        static byte[] Derivar(string pin, byte[] sal, int iteraciones)
        {
            using (var kdf = new Rfc2898DeriveBytes(pin, sal, iteraciones, HashAlgorithmName.SHA256))
                return kdf.GetBytes(32);
        }

        static byte[] Aleatorio(int n)
        {
            var b = new byte[n];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(b);
            return b;
        }

        static bool IgualesTiempoConstante(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        static string Base64UrlCodificar(byte[] datos)
        {
            return Convert.ToBase64String(datos).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        static byte[] Base64UrlDecodificar(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            switch (s.Length % 4)
            {
                case 2: s += "=="; break;
                case 3: s += "="; break;
            }
            return Convert.FromBase64String(s);
        }
    }
}
