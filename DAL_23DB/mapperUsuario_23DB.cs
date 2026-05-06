using BE_23DB;
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
    public class mapperUsuario_23DB
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

        public Usuario_23DB ObtenerUsuario_23DB(string login_23DB, string password_23DB)
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, Login, IdRol, Bloqueado, Activo FROM Usuario_23DB WHERE Login = @Login AND Password = @Password";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@Login", login_23DB);
                cmd_23DB.Parameters.AddWithValue("@Password", password_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if (reader_23DB.Read())
                {
                    usuario_23DB = new Usuario_23DB
                    {
                        DNI_23DB = reader_23DB["DNI"].ToString(),
                        Apellido_23DB = reader_23DB["Apellido"].ToString(),
                        Nombre_23DB = reader_23DB["Nombre"].ToString(),
                        Email_23DB = reader_23DB["Email"].ToString(),
                        Login_23DB = reader_23DB["Login"].ToString(),
                        IdRol_23DB = Convert.ToInt32(reader_23DB["IdRol"]),
                        Bloqueado_23DB = Convert.ToBoolean(reader_23DB["Bloqueado"]),
                        Activo_23DB = Convert.ToBoolean(reader_23DB["Activo"])
                    };
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return usuario_23DB;
        }

        public Usuario_23DB ObtenerUsuarioPorLogin_23DB(string login_23DB)
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, Login, IdRol, Bloqueado, Activo FROM Usuario_23DB WHERE Login = @Login";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@Login", login_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if (reader_23DB.Read())
                {
                    usuario_23DB = new Usuario_23DB
                    {
                        DNI_23DB = reader_23DB["DNI"].ToString(),
                        Apellido_23DB = reader_23DB["Apellido"].ToString(),
                        Nombre_23DB = reader_23DB["Nombre"].ToString(),
                        Email_23DB = reader_23DB["Email"].ToString(),
                        Login_23DB = reader_23DB["Login"].ToString(),
                        IdRol_23DB = Convert.ToInt32(reader_23DB["IdRol"]),
                        Bloqueado_23DB = Convert.ToBoolean(reader_23DB["Bloqueado"]),
                        Activo_23DB = Convert.ToBoolean(reader_23DB["Activo"])
                    };
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return usuario_23DB;
        }

        public void BloquearUsuario_23DB(string dni)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET Bloqueado = 1 WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }
    }
}
