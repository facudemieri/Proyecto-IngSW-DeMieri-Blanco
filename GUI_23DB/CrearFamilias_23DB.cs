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
    public partial class CrearFamilias_23DB : Form
    {
        public CrearFamilias_23DB()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            MenuPrincipal_23DB menuPrincipal_23DB = new MenuPrincipal_23DB();
            menuPrincipal_23DB.Show();
            this.Close();
        }
    }
}
