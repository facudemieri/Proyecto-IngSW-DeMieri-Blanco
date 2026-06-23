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
    public class mapperFamilia_23DB
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

        public List<Familia_23DB> ObtenerFamilias_23DB()
        {
            List<Familia_23DB> lista_23DB = new List<Familia_23DB>();
            try
            {
                Conectar_23DB();
                string query_23DB = "SELECT IdFamilia, NombreFamilia FROM Familia_23DB";
                SqlCommand cmd_23DB = new SqlCommand(query_23DB, conexion_23DB);
                SqlDataReader reader_23DB = cmd_23DB.ExecuteReader();
                while(reader_23DB.Read())
                {
                    lista_23DB.Add(new Familia_23DB
                    {
                        IdFamilia_23DB = (int)reader_23DB["IdFamilia"],
                        NombreFamilia_23DB = reader_23DB["NombreFamilia"].ToString()
                    });
                }
            }
            finally
            {
                Desconectar_23DB();
            }
            return lista_23DB;
        }

        public Familia_23DB ObtenerFamiliaCompleta_23DB(int idFamilia_23DB)
        {
            try
            {
                Conectar_23DB();
                return ObtenerFamiliaRecursiva_23DB(idFamilia_23DB);
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void SetConexion_23DB(SqlConnection conexion_23DB)
        {
            this.conexion_23DB = conexion_23DB;
        }

        public Familia_23DB ObtenerFamiliaRecursiva_23DB(int idFamilia_23DB)
        {
            string queryFamilia_23DB = "SELECT IdFamilia, NombreFamilia FROM Familia_23DB WHERE IdFamilia = @IdFamilia";
            SqlCommand cmdFamilia_23DB = new SqlCommand(queryFamilia_23DB, conexion_23DB);
            cmdFamilia_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
            SqlDataReader readerFamilia_23DB = cmdFamilia_23DB.ExecuteReader();
            Familia_23DB familia_23DB = null;
            if (readerFamilia_23DB.Read())
            {
                familia_23DB = new Familia_23DB
                {
                    IdFamilia_23DB = (int)readerFamilia_23DB["IdFamilia"],
                    NombreFamilia_23DB = readerFamilia_23DB["NombreFamilia"].ToString()
                };
            }
            readerFamilia_23DB.Close();

            if (familia_23DB == null) return null;

            
            string queryPatentes_23DB = "SELECT P.IdPatente, P.NombrePatente, P.Descripcion FROM Patente_23DB P INNER JOIN FamPat_23DB FP ON P.IdPatente = FP.IdPatente WHERE FP.IdFamilia = @IdFamilia";
            SqlCommand cmdPatentes_23DB = new SqlCommand(queryPatentes_23DB, conexion_23DB);
            cmdPatentes_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
            SqlDataReader readerPatentes_23DB = cmdPatentes_23DB.ExecuteReader();
            while (readerPatentes_23DB.Read())
            {
                familia_23DB.Agregar_23DB(new Patente_23DB
                {
                    IdPatente_23DB = (int)readerPatentes_23DB["IdPatente"],
                    NombrePatente_23DB = readerPatentes_23DB["NombrePatente"].ToString(),
                    Descripcion_23DB = readerPatentes_23DB["Descripcion"].ToString()
                });
            }
            readerPatentes_23DB.Close();

            // trae las familias de la familia
            string queryFamilias_23DB = "SELECT IdFamiliaHija FROM FamFam_23DB WHERE IdFamiliaPadre = @IdFamilia";
            SqlCommand cmdFamilias_23DB = new SqlCommand(queryFamilias_23DB, conexion_23DB);
            cmdFamilias_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
            SqlDataReader readerFamilias_23DB = cmdFamilias_23DB.ExecuteReader();
            List<int> idsFamilias_23DB = new List<int>();
            while (readerFamilias_23DB.Read())
            {
                idsFamilias_23DB.Add((int)readerFamilias_23DB["IdFamiliaHija"]);
            }                
            readerFamilias_23DB.Close();

            foreach (int idSubFamilia_23DB in idsFamilias_23DB)
            {
                familia_23DB.Agregar_23DB(ObtenerFamiliaRecursiva_23DB(idSubFamilia_23DB));
            }              
            return familia_23DB;
        }
        public void InsertarFamilia_23DB(string nombreFamilia_23DB, List<Rol_23DB> componentes_23DB)
        {
            try
            {
                Conectar_23DB();
                
                string queryId_23DB = "SELECT ISNULL(MAX(IdFamilia), 0) + 1 FROM Familia_23DB";
                SqlCommand cmdId_23DB = new SqlCommand(queryId_23DB, conexion_23DB);
                int nuevoId_23DB = (int)cmdId_23DB.ExecuteScalar();
                
                string queryFamilia_23DB = "INSERT INTO Familia_23DB (IdFamilia, NombreFamilia) VALUES (@IdFamilia, @NombreFamilia)";
                SqlCommand cmdFamilia_23DB = new SqlCommand(queryFamilia_23DB, conexion_23DB);
                cmdFamilia_23DB.Parameters.AddWithValue("@IdFamilia", nuevoId_23DB);
                cmdFamilia_23DB.Parameters.AddWithValue("@NombreFamilia", nombreFamilia_23DB);
                cmdFamilia_23DB.ExecuteNonQuery();

                
                foreach(Rol_23DB componente_23DB in componentes_23DB)
                {
                    if(componente_23DB is Patente_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO FamPat_23DB (IdFamilia, IdPatente) VALUES (@IdFamilia, @IdPatente)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamilia", nuevoId_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdPatente", ((Patente_23DB)componente_23DB).IdPatente_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                    else if(componente_23DB is Familia_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO FamFam_23DB (IdFamiliaPadre, IdFamiliaHija) VALUES (@IdFamiliaPadre, @IdFamiliaHija)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamiliaPadre", nuevoId_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamiliaHija", ((Familia_23DB)componente_23DB).IdFamilia_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void ModificarFamilia_23DB(int idFamilia_23DB, string nombreFamilia_23DB, List<Rol_23DB> componentes_23DB)
        {
            try
            {
                Conectar_23DB();

                
                string queryFamilia_23DB = "UPDATE Familia_23DB SET NombreFamilia = @NombreFamilia WHERE IdFamilia = @IdFamilia";
                SqlCommand cmdFamilia_23DB = new SqlCommand(queryFamilia_23DB, conexion_23DB);
                cmdFamilia_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdFamilia_23DB.Parameters.AddWithValue("@NombreFamilia", nombreFamilia_23DB);
                cmdFamilia_23DB.ExecuteNonQuery();

                
                string queryDelPat_23DB = "DELETE FROM FamPat_23DB WHERE IdFamilia = @IdFamilia";
                SqlCommand cmdDelPat_23DB = new SqlCommand(queryDelPat_23DB, conexion_23DB);
                cmdDelPat_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdDelPat_23DB.ExecuteNonQuery();

                string queryDelFam_23DB = "DELETE FROM FamFam_23DB WHERE IdFamiliaPadre = @IdFamilia";
                SqlCommand cmdDelFam_23DB = new SqlCommand(queryDelFam_23DB, conexion_23DB);
                cmdDelFam_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdDelFam_23DB.ExecuteNonQuery();

                
                foreach (Rol_23DB componente_23DB in componentes_23DB)
                {
                    if (componente_23DB is Patente_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO FamPat_23DB (IdFamilia, IdPatente) VALUES (@IdFamilia, @IdPatente)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdPatente", ((Patente_23DB)componente_23DB).IdPatente_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                    else if (componente_23DB is Familia_23DB)
                    {
                        string queryRel_23DB = "INSERT INTO FamFam_23DB (IdFamiliaPadre, IdFamiliaHija) VALUES (@IdFamiliaPadre, @IdFamiliaHija)";
                        SqlCommand cmdRel_23DB = new SqlCommand(queryRel_23DB, conexion_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamiliaPadre", idFamilia_23DB);
                        cmdRel_23DB.Parameters.AddWithValue("@IdFamiliaHija", ((Familia_23DB)componente_23DB).IdFamilia_23DB);
                        cmdRel_23DB.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                Desconectar_23DB();
            }
        }

        public void EliminarFamilia_23DB(int idFamilia_23DB)
        {
            try
            {
                Conectar_23DB();

                
                string queryDelRolFam_23DB = "DELETE FROM RolFam_23DB WHERE IdFamilia = @IdFamilia";
                SqlCommand cmdDelRolFam_23DB = new SqlCommand(queryDelRolFam_23DB, conexion_23DB);
                cmdDelRolFam_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdDelRolFam_23DB.ExecuteNonQuery();

                
                string queryDelPat_23DB = "DELETE FROM FamPat_23DB WHERE IdFamilia = @IdFamilia";
                SqlCommand cmdDelPat_23DB = new SqlCommand(queryDelPat_23DB, conexion_23DB);
                cmdDelPat_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdDelPat_23DB.ExecuteNonQuery();

                
                string queryDelFam_23DB = "DELETE FROM FamFam_23DB WHERE IdFamiliaPadre = @IdFamilia OR IdFamiliaHija = @IdFamilia";
                SqlCommand cmdDelFam_23DB = new SqlCommand(queryDelFam_23DB, conexion_23DB);
                cmdDelFam_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdDelFam_23DB.ExecuteNonQuery();

                // Eliminar Familia
                string queryFamilia_23DB = "DELETE FROM Familia_23DB WHERE IdFamilia = @IdFamilia";
                SqlCommand cmdFamilia_23DB = new SqlCommand(queryFamilia_23DB, conexion_23DB);
                cmdFamilia_23DB.Parameters.AddWithValue("@IdFamilia", idFamilia_23DB);
                cmdFamilia_23DB.ExecuteNonQuery();
            }
            finally
            {
                Desconectar_23DB();
            }
        }
    }
}
