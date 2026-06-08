using BLL_23DB;
using Services_23DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_23DB
{
    public partial class CrearFamilias_23DB : Form, IIdiomaObserver_23DB
    {
        
        public CrearFamilias_23DB()
        {
            InitializeComponent();
            Observer_23DB.ObtenerInstancia_23DB().Suscribir_23DB(this);
            AplicarIdiomaActual_23DB();
        }

        public void ActualizarIdioma_23DB(Dictionary<string, string> configuracion_23DB)
        {
            string formName_23DB = "CrearFamilias";
            foreach (Control control_23DB in ObtenerTodosControles_23DB(this))
            {
                string clave_23DB = formName_23DB + "_" + control_23DB.Name;
                if (configuracion_23DB.ContainsKey(clave_23DB))
                {
                    control_23DB.Text = configuracion_23DB[clave_23DB];
                }                    
            }

        }

        private List<Control> ObtenerTodosControles_23DB(Control control_23DB)
        {
            List<Control> lista_23DB = new List<Control>();
            foreach (Control c_23DB in control_23DB.Controls)
            {
                lista_23DB.Add(c_23DB);
                lista_23DB.AddRange(ObtenerTodosControles_23DB(c_23DB));
            }
            return lista_23DB;
        }

        private void CrearFamilias_23DB_FormClosing(object sender, FormClosingEventArgs e)
        {
            Observer_23DB.ObtenerInstancia_23DB().Desuscribir_23DB(this);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {        
            this.Close();
        }

        public void AplicarIdiomaActual_23DB()
        {
            string idiomaActual_23DB = SessionManager_23DB.ObtenerInstancia_23DB().UltimoIdioma_23DB;
            if (!string.IsNullOrEmpty(idiomaActual_23DB))
            {
                IdiomaBLL_23DB idiomaBLL_23DB = new IdiomaBLL_23DB();
                ActualizarIdioma_23DB(idiomaBLL_23DB.CargarConfiguracion_23DB(idiomaActual_23DB));
            }
        }
    }
}
