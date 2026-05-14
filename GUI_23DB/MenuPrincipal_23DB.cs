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
    public partial class MenuPrincipal_23DB : Form
    {

        public MenuPrincipal_23DB()
        {
            InitializeComponent();
            CargarDatosSesion_23DB();
        }

        private void CargarDatosSesion_23DB()
        {
            SessionManager_23DB sesion_23DB = SessionManager_23DB.ObtenerInstancia_23DB();
            lblLoginMp.Text = "Bienvenido: " + sesion_23DB.Login_23DB;
            lblRol.Text = "Rol: " + sesion_23DB.Rol_23DB;
        }

        private void MenuPrincipal_23DB_Load(object sender, EventArgs e)
        {
            
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            cmsUsuario.Show(btnUsuario, new System.Drawing.Point(btnUsuario.Width, 0));
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            cmsAdmin.Show(btnAdmin, new System.Drawing.Point(btnAdmin.Width, 0));
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña_23DB cambiarContraseñaForm_23DB = new CambiarContraseña_23DB();
            cambiarContraseñaForm_23DB.ShowDialog();
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarIdioma_23DB cambiarIdioma_23DB = new CambiarIdioma_23DB();
            cambiarIdioma_23DB.ShowDialog();
        }

        private void reloginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionManager_23DB.ObtenerInstancia_23DB().CerrarSesion_23DB();
            InicioSesion_23DB login_23DB = new InicioSesion_23DB();
            login_23DB.EsRelogin_23DB = true;
            DialogResult resultado_23DB = login_23DB.ShowDialog();

            if (resultado_23DB == DialogResult.OK)
                CargarDatosSesion_23DB();
            else
                this.Close();
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionUsuario_23DB gestionUsuarios_23DB = new GestionUsuario_23DB();
            gestionUsuarios_23DB.ShowDialog();
        }

        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuditoriaEventos_23DB auditEventos_23DB = new AuditoriaEventos_23DB();
            auditEventos_23DB.ShowDialog();
        }

        private void gestionDePerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función en desarrollo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado_23DB = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado_23DB == DialogResult.Yes)
            {
                EventoBLL_23DB eventoBLL_23DB = new EventoBLL_23DB();
                string dni_23DB = SessionManager_23DB.ObtenerInstancia_23DB().DNI_23DB;
                eventoBLL_23DB.RegistrarEvento_23DB(dni_23DB, "Usuarios", "Logout", 1);
                SessionManager_23DB.ObtenerInstancia_23DB().CerrarSesion_23DB();
                Application.Exit();
            }
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función en desarrollo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función en desarrollo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
