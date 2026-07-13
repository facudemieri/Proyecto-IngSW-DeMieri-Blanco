using Services_23DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_23DB
{
    public class mapperPatente_23DB
    {
        private SqlConnection conexion_23DB;

        private void Conectar_23DB()
        {
            conexion_23DB = new SqlConnection(Conexion_23DB.ObtenerCadena_23DB());
            conexion_23DB.Open();
        }

        private void Desconectar_23DB()
        {
            if(conexion_23DB != null && conexion_23DB.State == ConnectionState.Open)
            { 
                conexion_23DB.Close(); 
            }
        }

        public List<Patente_23DB> ObtenerPatentes_23DB()
        {
            List<Patente_23DB> lista_23DB = new List<Patente_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT IdPatente, NombrePatente, Descripcion FROM Patente_23DB";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while(reader_23DB.Read())
                {
                    lista_23DB.Add(new Patente_23DB
                    {
                        IdPatente_23DB = (int)reader_23DB["IdPatente"],
                        NombrePatente_23DB = reader_23DB["NombrePatente"].ToString(),
                        Descripcion_23DB = reader_23DB["Descripcion"].ToString()
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }
    }
}
