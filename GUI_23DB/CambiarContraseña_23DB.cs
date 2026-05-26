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
    public partial class CambiarContraseña_23DB : Form
    {
        public CambiarContraseña_23DB()
        {
            InitializeComponent();
        }

        private UsuarioBLL_23DB usuarioBLL_23DB = new UsuarioBLL_23DB();
        private EventoBLL_23DB eventoBLL_23DB = new EventoBLL_23DB();

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPasswordActual.Text) ||
                string.IsNullOrEmpty(txtPasswordNueva.Text) ||
                string.IsNullOrEmpty(txtConfirmarPassword.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPasswordNueva.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas nuevas no coinciden.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPasswordActual.Text == txtPasswordNueva.Text)
            {
                MessageBox.Show("La nueva contraseña debe ser distinta a la actual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dni_23DB = SessionManager_23DB.ObtenerInstancia_23DB().DNI_23DB;
            bool resultado_23DB = usuarioBLL_23DB.CambiarClave_23DB(dni_23DB, txtPasswordActual.Text, txtPasswordNueva.Text, txtConfirmarPassword.Text);

            if (!resultado_23DB)
            {
                MessageBox.Show("La contraseña actual es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            eventoBLL_23DB.RegistrarEvento_23DB(dni_23DB, "Usuarios", "Cambiar Clave", 2);
            MessageBox.Show("Contraseña actualizada correctamente. Debe iniciar sesión nuevamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SessionManager_23DB.ObtenerInstancia_23DB().CerrarSesion_23DB();
            this.Close();
            Application.OpenForms["MenuPrincipal_23DB"].Close();
            eventoBLL_23DB.RegistrarEvento_23DB(dni_23DB, "Usuarios", "Logout", 1);
            InicioSesion_23DB login_23DB = new InicioSesion_23DB();
            login_23DB.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkMostrarActual_CheckedChanged(object sender, EventArgs e)
        {
            txtPasswordActual.PasswordChar = chkMostrarActual.Checked ? '\0' : '*';
        }

        private void chkMostarNueva_CheckedChanged(object sender, EventArgs e)
        {
            txtPasswordNueva.PasswordChar = chkMostrarNueva.Checked ? '\0' : '*';
        }

        private void chkMostrarConfirmacion_CheckedChanged(object sender, EventArgs e)
        {
            txtConfirmarPassword.PasswordChar = chkMostrarConfirmacion.Checked ? '\0' : '*';
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void CambiarContraseña_23DB_Load(object sender, EventArgs e)
        {
            lblLogin.Text = "LOGIN: " + SessionManager_23DB.ObtenerInstancia_23DB().Login_23DB;
        }
    }
}
