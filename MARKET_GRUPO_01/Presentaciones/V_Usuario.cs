﻿using Capa_Datos.Modelos;
using Capa_Negocios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MARKET_GRUPO_01.Presentaciones
{
    public partial class V_Usuario : Form
    {
        N_Usuarios n_usuario;
        N_Roles n_roles;
        public V_Usuario()
        {
            InitializeComponent();
            n_usuario = new N_Usuarios();
            n_roles = new N_Roles();
        }
        private void Limpiar()
        {
            TxtIdUsuario.Text = "";
            TxtUsuario.Text = "";
            TxtNombre.Text = "";
            TxtApellido.Text = "";
            TxtContraseña.Text = "";
            TxtCorreo.Text = "";
            CmbRol.SelectedValue = -1;
            ChkActivo.Checked = false;
            errorProvider1.Clear();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string UsuarioId = TxtIdUsuario.Text;
            string Usuario = TxtUsuario.Text;
            string Nombre = TxtNombre.Text;
            string Apellido = TxtApellido.Text;
            string Contraseña = TxtContraseña.Text;
            string Correo = TxtCorreo.Text;
            int Rol = Convert.ToInt32(CmbRol.SelectedValue);
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrWhiteSpace(Usuario))
            {
                errorProvider1.SetError(TxtUsuario, "Debe colocar el codigo de la clasificacion");
                return;
            }

            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrWhiteSpace(Nombre))
            {
                errorProvider1.SetError(TxtNombre, "Debe colocar la descripcion");
                return;
            }
            if (string.IsNullOrEmpty(UsuarioId) || string.IsNullOrWhiteSpace(UsuarioId))
            {
                UsuarioId = "0";
            }
            var usuarios = new Usuarios();
            if (int.Parse(UsuarioId) != 0)
            {
                usuarios.ID = int.Parse(UsuarioId);
            }
            usuarios.Usuario = Usuario;
            usuarios.Nombre = Nombre;
            usuarios.Apellido = Apellido;
            usuarios.Contraseña = Contraseña;
            usuarios.Correo = Correo;
            usuarios.RolID = Rol;
            usuarios.Estado = ChkActivo.Checked;
            n_usuario.GuardarUsuario(usuarios);

            Limpiar();
        }
        void CmbRoles()
        {
            try
            {
                var roles = n_roles.ObtenerRoles();

                if (roles != null && roles.Any())
                {
                    CmbRol.DataSource = roles;
                    CmbRol.DisplayMember = "NombreRol";
                    CmbRol.ValueMember = "RolID";
                }
                else
                {
                    MessageBox.Show("No hay roles disponibles. Debe agregar roles.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al cargar los roles: {ex.Message}");
            }
        }

        private void V_Usuario_Load(object sender, EventArgs e)
        {
            CmbRoles();
        }
    }
}
