﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacientes
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
            DatabaseName = Program.Configuration.GetSection("DatabaseName").Get<string>();
        }

        public string DatabaseName { get; set; }
    }
}
