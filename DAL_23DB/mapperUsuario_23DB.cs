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
            if(conexion_23DB != null && conexion_23DB.State == ConnectionState.Open) 
            {
                conexion_23DB.Close();
            }
        }

        private Usuario_23DB MapearUsuario_23DB(SqlDataReader reader_23DB)
        {
            return new Usuario_23DB
            {
                DNI_23DB = reader_23DB["DNI"].ToString(),
                Apellido_23DB = reader_23DB["Apellido"].ToString(),
                Nombre_23DB = reader_23DB["Nombre"].ToString(),
                Email_23DB = reader_23DB["Email"].ToString(),
                Login_23DB = reader_23DB["Login"].ToString(),
                IdRol_23DB = (int)reader_23DB["IdRol"],
                Bloqueado_23DB = (bool)reader_23DB["Bloqueado"],
                Activo_23DB = (bool)reader_23DB["Activo"],
                IntentosFallidos_23DB = reader_23DB["IntentosFallidos"] == DBNull.Value ? 0 : (int)reader_23DB["IntentosFallidos"],
                FechaUltimoIntento_23DB = reader_23DB["FechaUltimoIntento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader_23DB["FechaUltimoIntento"]),
                UltimoIdioma_23DB = reader_23DB["UltimoIdioma"] == DBNull.Value ? string.Empty : reader_23DB["UltimoIdioma"].ToString()
            };
        }

        public List<Usuario_23DB> ObtenerTodos_23DB(string filtro_23DB)
        {
            List<Usuario_23DB> lista_23DB = new List<Usuario_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, [Login], IdRol, Bloqueado, Activo, IntentosFallidos, FechaUltimoIntento, UltimoIdioma FROM Usuario_23DB";
                if(filtro_23DB == "Activos")
                { 
                    query_23DB += " WHERE Activo = 1"; 
                }
                else if(filtro_23DB == "Inactivos")
                { 
                    query_23DB += " WHERE Activo = 0"; 
                }
                query_23DB += " ORDER BY Apellido, Nombre";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while(reader_23DB.Read())
                { 
                    lista_23DB.Add(MapearUsuario_23DB(reader_23DB)); 
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public void Insertar_23DB(Usuario_23DB usuario_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "INSERT INTO Usuario_23DB (DNI, Apellido, Nombre, Email, [Login], [Password], IdRol, Bloqueado, Activo) VALUES (@DNI, @Apellido, @Nombre, @Email, @Login, @Password, @IdRol, @Bloqueado, @Activo)";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", usuario_23DB.DNI_23DB);
                cmd_23DB.Parameters.AddWithValue("@Apellido", usuario_23DB.Apellido_23DB);
                cmd_23DB.Parameters.AddWithValue("@Nombre", usuario_23DB.Nombre_23DB);
                cmd_23DB.Parameters.AddWithValue("@Email", usuario_23DB.Email_23DB);
                cmd_23DB.Parameters.AddWithValue("@Login", usuario_23DB.Login_23DB);
                cmd_23DB.Parameters.AddWithValue("@Password", usuario_23DB.Password_23DB);
                cmd_23DB.Parameters.AddWithValue("@IdRol", usuario_23DB.IdRol_23DB);
                cmd_23DB.Parameters.AddWithValue("@Bloqueado", usuario_23DB.Bloqueado_23DB);
                cmd_23DB.Parameters.AddWithValue("@Activo", usuario_23DB.Activo_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void Modificar_23DB(Usuario_23DB usuario_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET Email = @Email, IdRol = @IdRol WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", usuario_23DB.DNI_23DB);
                cmd_23DB.Parameters.AddWithValue("@Email", usuario_23DB.Email_23DB);
                cmd_23DB.Parameters.AddWithValue("@IdRol", usuario_23DB.IdRol_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void CambiarEstado_23DB(string dni_23DB, bool activo_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET Activo = @Activo WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Activo", activo_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void Desbloquear_23DB(string dni_23DB, string passwordInicial_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET Bloqueado = 0, [Password] = @Password, IntentosFallidos = 0 WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Password", passwordInicial_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void ActualizarPassword_23DB(string dni_23DB, string passwordEncriptado_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET [Password] = @Password WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Password", passwordEncriptado_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }
        public Usuario_23DB ObtenerUsuario_23DB(string login_23DB, string password_23DB) // valido credenciales
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, [Login], IdRol, Bloqueado, Activo, IntentosFallidos, FechaUltimoIntento, UltimoIdioma FROM Usuario_23DB WHERE Login = @Login AND Password = @Password";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@Login", login_23DB);
                cmd_23DB.Parameters.AddWithValue("@Password", password_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while(reader_23DB.Read())
                {
                    usuario_23DB = MapearUsuario_23DB(reader_23DB);
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return usuario_23DB;
        }

        public Usuario_23DB ObtenerUsuarioPorLogin_23DB(string login_23DB) // traigo el dni del usuario antes de validar el password
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, [Login], IdRol, Bloqueado, Activo, IntentosFallidos, FechaUltimoIntento, UltimoIdioma FROM Usuario_23DB WHERE Login = @Login";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@Login", login_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while(reader_23DB.Read())
                {
                    usuario_23DB = MapearUsuario_23DB(reader_23DB);
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return usuario_23DB;
        }

        public Usuario_23DB ObtenerUsuarioPorDNI_23DB(string dni_23DB, string password_23DB) // para cambiar clave
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, [Login], IdRol, Bloqueado, Activo FROM Usuario_23DB WHERE DNI = @DNI AND [Password] = @Password";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Password", password_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if(reader_23DB.Read())
                { 
                    usuario_23DB = MapearUsuario_23DB(reader_23DB); 
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return usuario_23DB;
        }

        public Usuario_23DB ObtenerUsuarioDNI_23DB(string dni_23DB) 
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Apellido, Nombre, Email, [Login], IdRol, Bloqueado, Activo FROM Usuario_23DB WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if (reader_23DB.Read())
                {
                    usuario_23DB = MapearUsuario_23DB(reader_23DB);
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

        public void IncrementarIntentos_23DB(string dni_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET IntentosFallidos = IntentosFallidos + 1, FechaUltimoIntento = @Fecha WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void ResetearIntentos_23DB(string dni_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET IntentosFallidos = 0, FechaUltimoIntento = NULL WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void ActualizarUltimoIdioma_23DB(string dni_23DB, string idioma_23DB)
        {
            try
            {
                Conectar_23DB();
                string query_23DB = "UPDATE Usuario_23DB SET UltimoIdioma = @Idioma WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Idioma", idioma_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }
    }
}
