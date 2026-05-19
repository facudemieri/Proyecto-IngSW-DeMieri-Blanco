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
    public class mapperEvento_23DB
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

        public void InsertarEvento_23DB(string dni_23DB, string modulo_23DB, string evento_23DB, int criticidad_23DB)
        {
            try
            {
                Conectar_23DB();

                string queryId_23DB = "SELECT ISNULL(MAX(Id_Evento), 0) + 1 FROM Eventos_23DB";
                SqlCommand cmdId_23DB = new SqlCommand(queryId_23DB, conexion_23DB);
                int nuevoId_23DB = (int)cmdId_23DB.ExecuteScalar();

                string query_23DB = "INSERT INTO Eventos_23DB (Id_Evento ,DNI, Fecha, Hora, Modulo, Evento, Criticidad) VALUES (@id_Evento, @DNI, @Fecha, @Hora, @Modulo, @Evento, @Criticidad)";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@Id_Evento", nuevoId_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                cmd_23DB.Parameters.AddWithValue("@Fecha", DateTime.Now.Date);
                cmd_23DB.Parameters.AddWithValue("@Hora", DateTime.Now.TimeOfDay);
                cmd_23DB.Parameters.AddWithValue("@Modulo", modulo_23DB);
                cmd_23DB.Parameters.AddWithValue("@Evento", evento_23DB);
                cmd_23DB.Parameters.AddWithValue("@Criticidad", criticidad_23DB);
                cmd_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public List<Evento_23DB> ObtenerEventos_23DB(DateTime fechaInicio_23DB, DateTime fechaFin_23DB)
        {
            List<Evento_23DB> lista_23DB = new List<Evento_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT Id_Evento, DNI, Fecha, Hora, Modulo, Evento, Criticidad FROM Eventos_23DB WHERE Fecha BETWEEN @FechaInicio AND @FechaFin ORDER BY Fecha DESC, Hora DESC";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@FechaInicio", fechaInicio_23DB.Date);
                cmd_23DB.Parameters.AddWithValue("@FechaFin", fechaFin_23DB.Date);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while (reader_23DB.Read())
                {
                    lista_23DB.Add(new Evento_23DB
                    {
                        Id_Evento_23DB = Convert.ToInt32(reader_23DB["Id_Evento"]),
                        DNI_23DB = reader_23DB["DNI"].ToString(),
                        Fecha_23DB = Convert.ToDateTime(reader_23DB["Fecha"]),
                        Hora_23DB = TimeSpan.Parse(reader_23DB["Hora"].ToString()),
                        Modulo_23DB = reader_23DB["Modulo"].ToString(),
                        Evento23DB = reader_23DB["Evento"].ToString(),
                        Criticidad_23DB = Convert.ToInt32(reader_23DB["Criticidad"])
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public List<Evento_23DB> FiltrarEventos_23DB(string dni_23DB, DateTime fechaInicio_23DB, DateTime fechaFin_23DB, string modulo_23DB, string evento_23DB, int criticidad_23DB)
        {
            List<Evento_23DB> lista_23DB = new List<Evento_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT Id_Evento, DNI, Fecha, Hora, Modulo, Evento, Criticidad FROM Eventos_23DB WHERE Fecha BETWEEN @FechaInicio AND @FechaFin";

                if (!string.IsNullOrEmpty(dni_23DB))
                    query_23DB += " AND DNI = @DNI";
                if (!string.IsNullOrEmpty(modulo_23DB))
                    query_23DB += " AND Modulo = @Modulo";
                if (!string.IsNullOrEmpty(evento_23DB))
                    query_23DB += " AND Evento = @Evento";
                if (criticidad_23DB > 0)
                    query_23DB += " AND Criticidad = @Criticidad";

                query_23DB += " ORDER BY Fecha DESC, Hora DESC";

                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@FechaInicio", fechaInicio_23DB.Date);
                cmd_23DB.Parameters.AddWithValue("@FechaFin", fechaFin_23DB.Date);

                if (!string.IsNullOrEmpty(dni_23DB))
                    cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                if (!string.IsNullOrEmpty(modulo_23DB))
                    cmd_23DB.Parameters.AddWithValue("@Modulo", modulo_23DB);
                if (!string.IsNullOrEmpty(evento_23DB))
                    cmd_23DB.Parameters.AddWithValue("@Evento", evento_23DB);
                if (criticidad_23DB > 0)
                    cmd_23DB.Parameters.AddWithValue("@Criticidad", criticidad_23DB);

                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while (reader_23DB.Read())
                {
                    lista_23DB.Add(new Evento_23DB
                    {
                        Id_Evento_23DB = Convert.ToInt32(reader_23DB["Id_Evento"]),
                        DNI_23DB = reader_23DB["DNI"].ToString(),
                        Fecha_23DB = Convert.ToDateTime(reader_23DB["Fecha"]),
                        Hora_23DB = TimeSpan.Parse(reader_23DB["Hora"].ToString()),
                        Modulo_23DB = reader_23DB["Modulo"].ToString(),
                        Evento23DB = reader_23DB["Evento"].ToString(),
                        Criticidad_23DB = Convert.ToInt32(reader_23DB["Criticidad"])
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public List<Usuario_23DB> ObtenerLogins_23DB()
        {
            List<Usuario_23DB> lista_23DB = new List<Usuario_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Login FROM Usuario_23DB ORDER BY Login";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while (reader_23DB.Read())
                {
                    lista_23DB.Add(new Usuario_23DB
                    {
                        DNI_23DB = reader_23DB["DNI"].ToString(),
                        Login_23DB = reader_23DB["Login"].ToString()
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public Usuario_23DB ObtenerUsuarioPorDNI_23DB(string dni_23DB)
        {
            Usuario_23DB usuario_23DB = null;
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT DNI, Nombre, Apellido FROM Usuario_23DB WHERE DNI = @DNI";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                cmd_23DB.Parameters.AddWithValue("@DNI", dni_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                if (reader_23DB.Read())
                {
                    usuario_23DB = new Usuario_23DB
                    {
                        DNI_23DB = reader_23DB["DNI"].ToString(),
                        Nombre_23DB = reader_23DB["Nombre"].ToString(),
                        Apellido_23DB = reader_23DB["Apellido"].ToString()
                    };
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return usuario_23DB;
        }



    }
}
