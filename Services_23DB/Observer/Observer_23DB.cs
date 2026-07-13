using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_23DB
{
    public class Observer_23DB : ISujeto_23DB
    {
        private static Observer_23DB instancia_23DB;
        private List<IIdiomaObserver_23DB> suscriptores_23DB = new List<IIdiomaObserver_23DB>();

        private Observer_23DB() { }

        public static Observer_23DB ObtenerInstancia_23DB()
        {
            if (instancia_23DB == null)
            {
                instancia_23DB = new Observer_23DB();
            }
                
            return instancia_23DB;
        }

        public void Suscribir_23DB(IIdiomaObserver_23DB observer_23DB)
        {
            if(!suscriptores_23DB.Contains(observer_23DB))
            {
                suscriptores_23DB.Add(observer_23DB);
            }
                
        }

        public void Desuscribir_23DB(IIdiomaObserver_23DB observer_23DB)
        {
            suscriptores_23DB.Remove(observer_23DB);
        }

        public void Notificar_23DB(Dictionary<string, string> configuracion_23DB)
        {
            foreach(IIdiomaObserver_23DB suscriptor_23DB in suscriptores_23DB)
            {
                suscriptor_23DB.ActualizarIdioma_23DB(configuracion_23DB);
            }
                
        }
    }
}
