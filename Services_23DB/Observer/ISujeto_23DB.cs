using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_23DB
{
    public interface ISujeto_23DB
    {
        void Suscribir_23DB(IIdiomaObserver_23DB observer_23DB);
        void Desuscribir_23DB(IIdiomaObserver_23DB observer_23DB);
        void Notificar_23DB(Dictionary<string, string> configuracion_23DB);
    }
}
