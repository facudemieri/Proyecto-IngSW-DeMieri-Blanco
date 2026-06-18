using DAL_23DB;
using Services_23DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_23DB
{
    public class FamiliaBLL_23DB
    {
        private mapperFamilia_23DB mapperFamilia_23DB = new mapperFamilia_23DB();

        public List<Familia_23DB> ObtenerFamilias_23DB()
        {
            return mapperFamilia_23DB.ObtenerFamilias_23DB();
        }

        public void CrearFamilia_23DB(string nombreFamilia_23DB, List<Rol_23DB> componentes_23DB)
        {
            mapperFamilia_23DB.InsertarFamilia_23DB(nombreFamilia_23DB, componentes_23DB);
        }

        public bool ValidarNombre_23DB(string nombreFamilia_23DB, List<Familia_23DB> familias_23DB)
        {
            foreach(Familia_23DB familia_23DB in familias_23DB)
            {
                if(familia_23DB.NombreFamilia_23DB.ToLower() == nombreFamilia_23DB.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidarElementos_23DB(List<Rol_23DB> componentes_23DB)
        {
            return componentes_23DB != null && componentes_23DB.Count > 0;
        }

        public void ModificarFamilia_23DB(int idFamilia_23DB, string nombreFamilia_23DB, List<Rol_23DB> componentes_23DB)
        {
            mapperFamilia_23DB.ModificarFamilia_23DB(idFamilia_23DB, nombreFamilia_23DB, componentes_23DB);
        }

        public void EliminarFamilia_23DB(int idFamilia_23DB)
        {
            mapperFamilia_23DB.EliminarFamilia_23DB(idFamilia_23DB);
        }

        public Familia_23DB ObtenerFamiliaCompleta_23DB(int idFamilia_23DB)
        {
            return mapperFamilia_23DB.ObtenerFamiliaCompleta_23DB(idFamilia_23DB);
        }
    }
}
