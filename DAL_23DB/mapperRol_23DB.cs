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
                while(reader_23DB.Read())
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
                if(reader_23DB.Read())
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

        public void InsertarRol_23DB(string nombreRol_23DB, List<Rol_23DB> componentes_23DB)
        {
            try
            {
                Conectar_23DB();

                // Generar nuevo ID
                string queryId_23DB = "SELECT ISNULL(MAX(IdRol), 0) + 1 FROM Rol_23DB";
                SqlCommand cmdId_23DB = new SqlCommand(queryId_23DB, conexion_23DB);
                int nuevoId_23DB = (int)cmdId_23DB.ExecuteScalar();

                // Insertar Rol
                string queryRol_23DB = "INSERT INTO Rol_23DB (IdRol, NombreRol) VALUES (@IdRol, @NombreRol)";
                SqlCommand cmdRol_23DB = new SqlCommand(queryRol_23DB, conexion_23DB);
                cmdRol_23DB.Parameters.AddWithValue("@IdRol", nuevoId_23DB);
                cmdRol_23DB.Parameters.AddWithValue("@NombreRol", nombreRol_23DB);
                cmdRol_23DB.ExecuteNonQuery();

                // Insertar relaciones
                foreach(Rol_23DB componente_23DB in componentes_23DB)
                {
                    if(componente_23DB is Patente_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO RolPat_23DB (IdRol, IdPatente) VALUES (@IdRol, @IdPatente)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdRol", nuevoId_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdPatente", ((Patente_23DB)componente_23DB).IdPatente_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                    else if(componente_23DB is Familia_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO RolFam_23DB (IdRol, IdFamilia) VALUES (@IdRol, @IdFamilia)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdRol", nuevoId_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamilia", ((Familia_23DB)componente_23DB).IdFamilia_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void ModificarRol_23DB(int idRol_23DB, string nombreRol_23DB, List<Rol_23DB> componentes_23DB)
        {
            try
            {
                Conectar_23DB();

                // Actualizar nombre
                string queryRol_23DB = "UPDATE Rol_23DB SET NombreRol = @NombreRol WHERE IdRol = @IdRol";
                SqlCommand cmdRol_23DB = new SqlCommand(queryRol_23DB, conexion_23DB);
                cmdRol_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                cmdRol_23DB.Parameters.AddWithValue("@NombreRol", nombreRol_23DB);
                cmdRol_23DB.ExecuteNonQuery();

                // Eliminar relaciones anteriores
                string queryDelPat_23DB = "DELETE FROM RolPat_23DB WHERE IdRol = @IdRol";
                SqlCommand cmdDelPat_23DB = new SqlCommand(queryDelPat_23DB, conexion_23DB);
                cmdDelPat_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                cmdDelPat_23DB.ExecuteNonQuery();

                string queryDelFam_23DB = "DELETE FROM RolFam_23DB WHERE IdRol = @IdRol";
                SqlCommand cmdDelFam_23DB = new SqlCommand(queryDelFam_23DB, conexion_23DB);
                cmdDelFam_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                cmdDelFam_23DB.ExecuteNonQuery();

                // Insertar nuevas relaciones
                foreach (Rol_23DB componente_23DB in componentes_23DB)
                {
                    if(componente_23DB is Patente_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO RolPat_23DB (IdRol, IdPatente) VALUES (@IdRol, @IdPatente)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdPatente", ((Patente_23DB)componente_23DB).IdPatente_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                    else if(componente_23DB is Familia_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO RolFam_23DB (IdRol, IdFamilia) VALUES (@IdRol, @IdFamilia)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamilia", ((Familia_23DB)componente_23DB).IdFamilia_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void EliminarRol_23DB(int idRol_23DB)
        {
            try
            {
                Conectar_23DB();

                string queryDelPat_23DB = "DELETE FROM RolPat_23DB WHERE IdRol = @IdRol";
                SqlCommand cmdDelPat_23DB = new SqlCommand(queryDelPat_23DB, conexion_23DB);
                cmdDelPat_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                cmdDelPat_23DB.ExecuteNonQuery();

                string queryDelFam_23DB = "DELETE FROM RolFam_23DB WHERE IdRol = @IdRol";
                SqlCommand cmdDelFam_23DB = new SqlCommand(queryDelFam_23DB, conexion_23DB);
                cmdDelFam_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                cmdDelFam_23DB.ExecuteNonQuery();

                string queryRol_23DB = "DELETE FROM Rol_23DB WHERE IdRol = @IdRol";
                SqlCommand cmdRol_23DB = new SqlCommand(queryRol_23DB, conexion_23DB);
                cmdRol_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                cmdRol_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public Rol_23DB ObtenerRolCompleto_23DB(int idRol_23DB)
        {
            Rol_23DB rol_23DB = null;
            try
            {
                Conectar_23DB();

                // Obtener el rol
                string queryRol_23DB = "SELECT IdRol, NombreRol FROM Rol_23DB WHERE IdRol = @IdRol";
                SqlCommand cmdRol_23DB = new SqlCommand(queryRol_23DB, conexion_23DB);
                cmdRol_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                SqlDataReader readerRol_23DB = cmdRol_23DB.ExecuteReader();
                if (readerRol_23DB.Read())
                {
                    rol_23DB = new Rol_23DB
                    {
                        IdRol_23DB = (int)readerRol_23DB["IdRol"],
                        NombreRol_23DB = readerRol_23DB["NombreRol"].ToString()
                    };
                }
                readerRol_23DB.Close();

                if (rol_23DB == null) return null;

                // Obtener patentes del rol
                string queryPatentes_23DB = "SELECT P.IdPatente, P.NombrePatente, P.Descripcion FROM Patente_23DB P INNER JOIN RolPat_23DB RP ON P.IdPatente = RP.IdPatente WHERE RP.IdRol = @IdRol";
                SqlCommand cmdPatentes_23DB = new SqlCommand(queryPatentes_23DB, conexion_23DB);
                cmdPatentes_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                SqlDataReader readerPatentes_23DB = cmdPatentes_23DB.ExecuteReader();
                while (readerPatentes_23DB.Read())
                {
                    rol_23DB.Agregar_23DB(new Patente_23DB
                    {
                        IdPatente_23DB = (int)readerPatentes_23DB["IdPatente"],
                        NombrePatente_23DB = readerPatentes_23DB["NombrePatente"].ToString(),
                        Descripcion_23DB = readerPatentes_23DB["Descripcion"].ToString()
                    });
                }
                readerPatentes_23DB.Close();

                // Obtener IDs de familias del rol
                string queryFamilias_23DB = "SELECT RF.IdFamilia FROM RolFam_23DB RF WHERE RF.IdRol = @IdRol";
                SqlCommand cmdFamilias_23DB = new SqlCommand(queryFamilias_23DB, conexion_23DB);
                cmdFamilias_23DB.Parameters.AddWithValue("@IdRol", idRol_23DB);
                SqlDataReader readerFamilias_23DB = cmdFamilias_23DB.ExecuteReader();
                List<int> idsFamilias_23DB = new List<int>();
                while (readerFamilias_23DB.Read())
                    idsFamilias_23DB.Add((int)readerFamilias_23DB["IdFamilia"]);
                readerFamilias_23DB.Close();

                // Cargar cada familia completa (recursiva) compartiendo la conexión
                mapperFamilia_23DB mapperFam_23DB = new mapperFamilia_23DB();
                mapperFam_23DB.SetConexion_23DB(conexion_23DB);
                foreach (int idFam_23DB in idsFamilias_23DB)
                {
                    Familia_23DB familia_23DB = mapperFam_23DB.ObtenerFamiliaRecursiva_23DB(idFam_23DB);
                    if (familia_23DB != null)
                        rol_23DB.Agregar_23DB(familia_23DB);
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

