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

namespace linq.VISTA.formularioBusqueda
{
    public partial class FrmBusqueda : Form
    {
        public FrmBusqueda()
        {
            InitializeComponent();
        }

        private void FrmBusqueda_Load(object sender, EventArgs e)
        {
            filtro();
        }

        void filtro() 
        {
           using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                String nombre = txtBusqueda.Text;
                var buscar = from tbprod in db.producto
                             where tbprod.nombreProducto.Contains(nombre)

                             select new
                             {
                                 Id = tbprod.idProducto,
                                 Nombre = tbprod.nombreProducto,
                                 Precio = tbprod.precioProducto


                             };

                dtvBusqueda.DataSource = buscar.ToList();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }

        private void dtvBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            envio();
        }


        void envio() 
        {
            String Id = dtvBusqueda.CurrentRow.Cells[0].Value.ToString();
            String Nombre = dtvBusqueda.CurrentRow.Cells[1].Value.ToString();
            String Precio = dtvBusqueda.CurrentRow.Cells[2].Value.ToString();


            frmMenu.ventas.txtIdProdcuto.Text = Id;
            frmMenu.ventas.txtProducto.Text = Nombre;
            frmMenu.ventas.txtPrecioProducto.Text = Precio;


            frmMenu.ventas.txtCantidad.Focus();
            this.Close();
        }

        private void dtvBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                envio();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
