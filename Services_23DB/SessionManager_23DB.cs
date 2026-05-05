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

        public string DNI_23DB { get; private set; }
        public string Nombre_23DB { get; private set; }
        public string Apellido_23DB { get; private set; }
        public string Rol_23DB { get; private set; }

        public SessionManager_23DB() { }

        public static SessionManager_23DB ObtenerInstancia_23DB()
        {
            if(instancia_23DB == null)
            {
                instancia_23DB = new SessionManager_23DB();
            }
            return instancia_23DB;
        }

        public void InicializarSesion_23DB(string dni_23DB, string nombre_23DB, string apellido_23DB, string rol_23DB)
        {
            DNI_23DB = dni_23DB;
            Nombre_23DB = nombre_23DB;
            Apellido_23DB = apellido_23DB;
            Rol_23DB = rol_23DB;
        }

        public void CerrarSesion_23DB()
        {
            instancia_23DB = null;
        }

    }
}
