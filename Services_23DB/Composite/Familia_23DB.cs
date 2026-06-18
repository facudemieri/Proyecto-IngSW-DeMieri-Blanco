using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_23DB
{
    public class Familia_23DB : Rol_23DB
    {
        private int idFamilia_;
        public int IdFamilia_23DB
        {
            get { return idFamilia_; }
            set { idFamilia_ = value; }
        }

        private string nombreFamilia_;
        public string NombreFamilia_23DB
        {
            get { return nombreFamilia_; }
            set { nombreFamilia_ = value; }
        }

        public override string ObtenerNombre_23DB()
        {
            return nombreFamilia_;
        }

        public override List<Rol_23DB> ObtenerHijos_23DB()
        {
            return Componentes_23DB;
        }

        public override void Agregar_23DB(Rol_23DB componente_23DB)
        {
            Componentes_23DB.Add(componente_23DB);
        }

        public override void Quitar_23DB(Rol_23DB componente_23DB)
        {
            Componentes_23DB.Remove(componente_23DB);
        }

        public override string ToString()
        {
            return "[Familia] " + nombreFamilia_;
        }
    }
}
