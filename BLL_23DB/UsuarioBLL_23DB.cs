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

        public List<Usuario_23DB> ObtenerTodos_23DB(string filtro_23DB)
        {
            return mapperUsuario_23DB.ObtenerTodos_23DB(filtro_23DB);
        }

        public void CrearUsuario_23DB(Usuario_23DB usuario_23DB)
        {
            usuario_23DB.Login_23DB = GenerarLogin_23DB(usuario_23DB.Nombre_23DB, usuario_23DB.Apellido_23DB);
            usuario_23DB.Password_23DB = GenerarPasswordInicial_23DB(usuario_23DB.DNI_23DB, usuario_23DB.Apellido_23DB);
            usuario_23DB.Bloqueado_23DB = false;
            usuario_23DB.Activo_23DB = true;
            mapperUsuario_23DB.Insertar_23DB(usuario_23DB);
        }

        public void ModificarUsuario_23DB(Usuario_23DB usuario_23DB)
        {
            mapperUsuario_23DB.Modificar_23DB(usuario_23DB);
        }

        public void CambiarEstado_23DB(string dni_23DB, bool activo_23DB)
        {
            mapperUsuario_23DB.CambiarEstado_23DB(dni_23DB, activo_23DB);
        }

        public void DesbloquearUsuario_23DB(string dni_23DB, string apellido_23DB)
        {
            string passwordInicial_23DB = GenerarPasswordInicial_23DB(dni_23DB, apellido_23DB);
            mapperUsuario_23DB.Desbloquear_23DB(dni_23DB, passwordInicial_23DB);
        }

        public bool CambiarClave_23DB(string dni_23DB, string passwordActual_23DB, string passwordNuevo_23DB, string confirmarPassword_23DB)
        {
            if (passwordNuevo_23DB != confirmarPassword_23DB)
                return false;
            if (passwordActual_23DB == passwordNuevo_23DB)
                return false;
            
            string passwordActualEncriptado_23DB = CryptoManager_23DB.EncriptarHash_23DB(passwordActual_23DB);
            Usuario_23DB usuario_23DB = mapperUsuario_23DB.ObtenerUsuarioPorDNI_23DB(
                SessionManager_23DB.ObtenerInstancia_23DB().DNI_23DB,
                passwordActualEncriptado_23DB
            );
            if (usuario_23DB == null)
                return false;
            string passwordNuevoEncriptado_23DB = CryptoManager_23DB.EncriptarHash_23DB(passwordNuevo_23DB);
            mapperUsuario_23DB.ActualizarPassword_23DB(dni_23DB, passwordNuevoEncriptado_23DB);
            return true;
        }
    }
}
