using BLL_23DB;
using BE_23DB;
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
    public partial class InicioSesion_23DB : Form
    {
        private UsuarioBLL_23DB usuarioBLL_23DB = new UsuarioBLL_23DB();
        private EventoBLL_23DB eventoBLL_23DB = new EventoBLL_23DB();
        private int intentosFallidos_23DB = 0;
        public bool EsRelogin_23DB { get; set; } = false;
        public InicioSesion_23DB()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            Usuario_23DB usuarioPorLogin_23DB = usuarioBLL_23DB.ObtenerUsuarioPorLogin_23DB(txtUsuario.Text);

            
            if (usuarioPorLogin_23DB == null)
            {
                intentosFallidos_23DB++;
                if (intentosFallidos_23DB >= 3)
                {
                    MessageBox.Show("Demasiados intentos fallidos. Contacte al administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                MessageBox.Show($"Credenciales incorrectas. Intentos restantes: {3 - intentosFallidos_23DB}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (usuarioPorLogin_23DB.Bloqueado_23DB)
            {
                MessageBox.Show("Su cuenta está bloqueada. Contacte al administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (!usuarioPorLogin_23DB.Activo_23DB)
            {
                MessageBox.Show("Su cuenta está deshabilitada. Contacte al administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            Usuario_23DB usuarioAutenticado_23DB = usuarioBLL_23DB.AutenticarUsuario_23DB(txtUsuario.Text, txtContraseña.Text);

            
            if (usuarioAutenticado_23DB == null)
            {
                intentosFallidos_23DB++;
                if (intentosFallidos_23DB >= 3)
                {
                    usuarioBLL_23DB.BloquearUsuario_23DB(usuarioPorLogin_23DB.DNI_23DB);
                    eventoBLL_23DB.RegistrarEvento_23DB(usuarioPorLogin_23DB.DNI_23DB, "Usuarios", "Bloqueo Automático por Intentos", 1);
                    MessageBox.Show("Su cuenta ha sido bloqueada por demasiados intentos fallidos. Contacte al administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                MessageBox.Show($"Credenciales incorrectas. Intentos restantes: {3 - intentosFallidos_23DB}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nombreRol_23DB = usuarioBLL_23DB.ObtenerNombreRol_23DB(usuarioAutenticado_23DB.IdRol_23DB);

            SessionManager_23DB.ObtenerInstancia_23DB().InicializarSesion_23DB(
                usuarioAutenticado_23DB.DNI_23DB,
                usuarioAutenticado_23DB.Login_23DB,    
                nombreRol_23DB
            );

            
            eventoBLL_23DB.RegistrarEvento_23DB(usuarioAutenticado_23DB.DNI_23DB, "Usuarios", "Login", 1);

            if (EsRelogin_23DB)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MenuPrincipal_23DB menuPrincipal_23DB = new MenuPrincipal_23DB();
                menuPrincipal_23DB.Show();
                this.Hide();
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtContraseña.PasswordChar = '\0';
            else
                txtContraseña.PasswordChar = '*';
        }
    }
}
