using BE_23DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_23DB;
using Services_23DB;

namespace BLL_23DB
{
    public class UsuarioBLL_23DB
    {
        private mapperUsuario_23DB mapperUsuario_23DB = new mapperUsuario_23DB();
        private RolBLL_23DB rolBLL_23DB = new RolBLL_23DB();

        public Usuario_23DB AutenticarUsuario_23DB(string login_23DB, string password_23DB)
        {
            string passwordEncriptado_23DB = CryptoManager_23DB.EncriptarHash_23DB(password_23DB);
            return mapperUsuario_23DB.ObtenerUsuario_23DB(login_23DB, passwordEncriptado_23DB);
        }

        public bool ValidarEstado_23DB(Usuario_23DB usuario_23DB)
        {
            if (usuario_23DB == null)
                return false;
            if (!usuario_23DB.Activo_23DB)
                return false;
            if (usuario_23DB.Bloqueado_23DB)
                return false;
            return true;
        }

        public void BloquearUsuario_23DB(string dni_23DB)
        {
            mapperUsuario_23DB.BloquearUsuario_23DB(dni_23DB);
        }

        public string ObtenerNombreRol_23DB(int idRol_23DB)
        {
            Rol_23DB rol_23DB = rolBLL_23DB.ObtenerRol_23DB(idRol_23DB);
            return rol_23DB != null ? rol_23DB.NombreRol_23DB : string.Empty;
        }
        public Usuario_23DB ObtenerUsuarioPorLogin_23DB(string login_23DB)
        {
            return mapperUsuario_23DB.ObtenerUsuarioPorLogin_23DB(login_23DB);
        }
        public string GenerarLogin_23DB(string nombre_23DB, string apellido_23DB)
        {
            return CryptoManager_23DB.GenerarLogin_23DB(nombre_23DB, apellido_23DB);
        }

        public string GenerarPasswordInicial_23DB(string dni_23DB, string apellido_23DB)
        {
            return CryptoManager_23DB.GenerarPassword_23DB(dni_23DB, apellido_23DB);
        }
    }
}
