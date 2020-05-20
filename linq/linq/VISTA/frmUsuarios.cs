using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using linq.Model;

namespace linq.VISTA
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }
        tb_usuarios user = new tb_usuarios();

        void cargardatos()
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                var tb_usuarios = db.tb_usuarios;
                foreach (var iterarDatosTbUsuarios in tb_usuarios)
                {
                    dtvUsuarios.Rows.Add(iterarDatosTbUsuarios.Id, iterarDatosTbUsuarios.Email, iterarDatosTbUsuarios.Contrasena);
                }

                //dtvUsuarios.DataSource = db.tb_usuarios.ToList();
            }
        }

        void limpiardatos() 
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities()) 
            {
                
                user.Email = txtUsuario.Text;
                user.Contrasena = txtContraseña.Text;

                db.tb_usuarios.Add(user);
                db.SaveChanges();
               
            }
            cargardatos();
            limpiardatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities()) 
            {
                String Id = dtvUsuarios.CurrentRow.Cells[0].Value.ToString();
                user = db.tb_usuarios.Find(int.Parse(Id));
                db.tb_usuarios.Remove(user);
                db.SaveChanges();
               
            } 
            cargardatos();
            limpiardatos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                String Id = dtvUsuarios.CurrentRow.Cells[0].Value.ToString();
                int idC = int.Parse(Id);
                user = db.tb_usuarios.Where(VerificarId => VerificarId.Id == idC).First();
                user.Email = txtUsuario.Text;
                user.Contrasena = txtContraseña.Text;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                
            }
            cargardatos();
            limpiardatos();
            }

        private void dtvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String Email = dtvUsuarios.CurrentRow.Cells[1].Value.ToString();
            String contraseña = dtvUsuarios.CurrentRow.Cells[2].Value.ToString();
            txtUsuario.Text = Email;
            txtContraseña.Text = contraseña;
            
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            cargardatos();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                cargardatos();
                limpiardatos();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
