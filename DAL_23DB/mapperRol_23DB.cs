using BE_23DB;
using DAL_23DB;
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
    public class mapperRol_23DB
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
            {
                conexion_23DB.Close();
            }
        }

        public List<Rol_23DB> ObtenerRoles_23DB()
        {
            List<Rol_23DB> lista_23DB = new List<Rol_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT IdRol, NombreRol FROM Rol_23DB";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while (reader_23DB.Read())
                {
                    lista_23DB.Add(new Rol_23DB
                    {
                        IdRol_23DB = (int)reader_23DB["IdRol"],
                        NombreRol_23DB = reader_23DB["NombreRol"].ToString()
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public Rol_23DB ObtenerRol_23DB(int idRol_23DB)
        {
            Rol_23DB rol_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT IdRol, NombreRol FROM Rol_23DB WHERE IdRol = @IdRol";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if (reader_23DB.Read())
                {
                    rol_23DB = new Rol_23DB
                    {
                        IdRol_23DB = (int)reader_23DB["IdRol"],
                        NombreRol_23DB = reader_23DB["NombreRol"].ToString()
                    };
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return rol_23DB;
        }
    }
}

