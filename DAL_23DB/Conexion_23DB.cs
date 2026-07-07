using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_23DB
{
    public class Conexion_23DB
    {
        //private static string cadena_23DB = @"Server=.;Database=INGSW_23DB;Integrated Security=True;";

        public static string ObtenerCadena_23DB()
        {
            return ConfigurationManager.ConnectionStrings["INGSW_23DB"].ConnectionString;
        }
    }
}
