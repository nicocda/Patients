
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Fichero
{
    public class Settings
    {
        private static Settings _Instance = null;
        public static Settings Properties
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Settings();

                return _Instance;
            }
        }

        public Settings()
        {
            DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
        }

        public string DatabaseName { get; set; }
    }
}
