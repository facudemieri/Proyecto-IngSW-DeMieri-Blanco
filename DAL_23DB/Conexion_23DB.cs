using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DAL_23DB
{
    public class Conexion_23DB
    {
        //private static string cadena_23DB = @"Server=.;Database=INGSW_23DB;Integrated Security=True;";

        public static string ObtenerCadena_23DB()
        {
            string servidor_23DB = ".";

            using (RegistryKey baseKey_23DB = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using (RegistryKey key_23DB = baseKey_23DB.OpenSubKey(@"SOFTWARE\AeroManager"))
            {
                if (key_23DB != null)
                    servidor_23DB = key_23DB.GetValue("Server", ".").ToString();
            }

            return $"Server={servidor_23DB};Database=INGSW_23DB;Integrated Security=True;";
        }
    }
}
