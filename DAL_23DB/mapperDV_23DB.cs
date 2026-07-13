using System;
using Services_23DB;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_23DB
{
    public class mapperDV_23DB
    {
        private SqlConnection conexion_23DB;

        private void Conectar_23DB()
        {
            conexion_23DB = new SqlConnection(Conexion_23DB.ObtenerCadena_23DB());
            conexion_23DB.Open();
        }

        private void Desconectar_23DB()
        {
            if (conexion_23DB != null && conexion_23DB.State == ConnectionState.Open)
                conexion_23DB.Close();
        }

        public void ActualizarDV_23DB(int idTabla_23DB, long dvh_23DB, long dvv_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE DV_23DB SET DVH = @DVH, DVV = @DVV WHERE IdTabla = @IdTabla";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@IdTabla", idTabla_23DB);
                cmd_23DB.Parameters.AddWithValue("@DVH", dvh_23DB);
                cmd_23DB.Parameters.AddWithValue("@DVV", dvv_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public DV_23DB ObtenerDV_23DB(int idTabla_23DB)
        {
            DV_23DB dv_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT IdTabla, NombreTabla, DVH, DVV FROM DV_23DB WHERE IdTabla = @IdTabla";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@IdTabla", idTabla_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if(reader_23DB.Read())
                {
                    dv_23DB = new DV_23DB
                    {
                        IdTabla_23DB = (int)reader_23DB["IdTabla"],
                        NombreTabla_23DB = reader_23DB["NombreTabla"].ToString(),
                        DVH_23DB = (long)reader_23DB["DVH"],
                        DVV_23DB = (long)reader_23DB["DVV"]
                    };
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return dv_23DB;
        }

        public List<DV_23DB> ObtenerTodosDV_23DB()
        {
            List<DV_23DB> lista_23DB = new List<DV_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT IdTabla, NombreTabla, DVH, DVV FROM DV_23DB";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while(reader_23DB.Read())
                {
                    lista_23DB.Add(new DV_23DB
                    {
                        IdTabla_23DB = (int)reader_23DB["IdTabla"],
                        NombreTabla_23DB = reader_23DB["NombreTabla"].ToString(),
                        DVH_23DB = (long)reader_23DB["DVH"],
                        DVV_23DB = (long)reader_23DB["DVV"]
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public DataTable ObtenerDatosTabla_23DB(string nombreTabla_23DB)
        {
            DataTable tabla_23DB = new DataTable();
            try
            {
                Conectar_23DB();
                string query_23DB = $"SELECT * FROM {nombreTabla_23DB}";
                SqlDataAdapter adapter_23DB = new SqlDataAdapter(query_23DB, conexion_23DB);
                adapter_23DB.Fill(tabla_23DB);
            }
            finally
            {
                Desconectar_23DB();
            }
            return tabla_23DB;
        }
    }
}

