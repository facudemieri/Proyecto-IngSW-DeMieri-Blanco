using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_23DB
{
    public class Rol_23DB
    {
        private int idRol_;

        public int IdRol_23DB
        {
            get { return idRol_; }
            set { idRol_ = value; }
        }

        private string nombreRol_;

        public string NombreRol_23DB
        {
            get { return nombreRol_; }
            set { nombreRol_ = value; }
        }

        private List<Rol_23DB> componentes_ = new List<Rol_23DB>();
        public List<Rol_23DB> Componentes_23DB
        {
            get { return componentes_; }
            set { componentes_ = value; }
        }

        public virtual string ObtenerNombre_23DB()
        {
            return nombreRol_;
        }

        public virtual List<Rol_23DB> ObtenerHijos_23DB()
        {
            return componentes_;
        }

        public virtual void Agregar_23DB(Rol_23DB componente_23DB)
        {
            componentes_.Add(componente_23DB);
        }

        public virtual void Quitar_23DB(Rol_23DB componente_23DB)
        {
            componentes_.Remove(componente_23DB);
        }

        public override string ToString()
        {
            return "[Rol] " + ObtenerNombre_23DB();
        }

    }
}
