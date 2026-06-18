using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_23DB
{
    public class Patente_23DB : Rol_23DB
    {
        private int idPatente_;
        public int IdPatente_23DB
        {
            get { return idPatente_; }
            set { idPatente_ = value; }
        }

        private string nombrePatente_;
        public string NombrePatente_23DB
        {
            get { return nombrePatente_; }
            set { nombrePatente_ = value; }
        }

        private string descripcion_;
        public string Descripcion_23DB
        {
            get { return descripcion_; }
            set { descripcion_ = value; }
        }

        public override string ObtenerNombre_23DB()
        {
            return nombrePatente_;
        }

        public override List<Rol_23DB> ObtenerHijos_23DB()
        {
            return new List<Rol_23DB>();
        }

        public override string ToString()
        {
            return "[Patente] " + nombrePatente_;
        }
    }
}
