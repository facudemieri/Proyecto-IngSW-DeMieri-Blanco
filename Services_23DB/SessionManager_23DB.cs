using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_23DB
{
    public class SessionManager_23DB
    {
        private static SessionManager_23DB instancia_23DB;
        private static readonly object lock_23DB = new object();

        public string DNI_23DB { get; private set; }
        public string Login_23DB { get; private set; }
        public string Rol_23DB { get; private set; }
        public string UltimoIdioma_23DB { get; set; }

        private SessionManager_23DB() { }

        public static SessionManager_23DB ObtenerInstancia_23DB()
        {
            if(instancia_23DB == null)
            {
                lock (lock_23DB)
                {
                    if(instancia_23DB == null)
                        instancia_23DB = new SessionManager_23DB();
                }
            }
            return instancia_23DB;
        }

        public void InicializarSesion_23DB(string dni_23DB, string login_23DB, string rol_23DB)
        {
            DNI_23DB = dni_23DB;
            Login_23DB = login_23DB;
            Rol_23DB = rol_23DB;
            UltimoIdioma_23DB = string.Empty;
        }

        public void CerrarSesion_23DB()
        {
            lock (lock_23DB)
            {
                instancia_23DB = null;
            }
        }

    }
}
