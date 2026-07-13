using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_23DB
{
    public class mapperRespaldo_23DB
    {
        public void GenerarBackup_23DB(string rutaCompleta_23DB)
        {
            string query_23DB = $"BACKUP DATABASE INGSW_23DB TO DISK = '{rutaCompleta_23DB}'";
            using(SqlConnection conn_23DB = new SqlConnection(Conexion_23DB.ObtenerCadena_23DB()))
            {
                conn_23DB.Open();
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conn_23DB);
                cmd_23DB.CommandTimeout = 120;
                cmd_23DB.ExecuteNonQuery();
            }
        }

        public void RestaurarBackup_23DB(string rutaArchivo_23DB)
        {
            // Conectarse a master para poder restaurar INGSW_23DB
            string cadenamaster_23DB = Conexion_23DB.ObtenerCadena_23DB().Replace("INGSW_23DB", "master");

            string query_23DB = $@"ALTER DATABASE INGSW_23DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE INGSW_23DB FROM DISK = '{rutaArchivo_23DB}' WITH REPLACE; ALTER DATABASE INGSW_23DB SET MULTI_USER;";

            using(SqlConnection conn_23DB = new SqlConnection(cadenamaster_23DB))
            {
                conn_23DB.Open();
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conn_23DB);
                cmd_23DB.CommandTimeout = 300;
                cmd_23DB.ExecuteNonQuery();
            }

        }
    }
}
