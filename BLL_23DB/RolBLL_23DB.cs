using BE_23DB;
using DAL_23DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_23DB
{
    public class RolBLL_23DB
    {
        private mapperRol_23DB rolDAL_23DB = new mapperRol_23DB();

        public List<Rol_23DB> ObtenerRoles_23DB()
        {
            return rolDAL_23DB.ObtenerRoles_23DB();
        }

        public Rol_23DB ObtenerRol_23DB(int idRol_23DB)
        {
            return rolDAL_23DB.ObtenerRol_23DB(idRol_23DB);
        }
    }
}
