using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using linq.VISTA.formularioBusqueda;
using linq.Model;

namespace linq.VISTA
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        void retornoId() 
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities()) 
            {
                var tb_v = db.tb_venta;
                
                foreach(var iterarDatos in tb_v) 
                {
                    txtNumVenta.Text = iterarDatos.idVenta.ToString();
                }
            }
        }
        void CargarCombo() 
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                var clientes = db.tb_cliente.ToList();
                cmbCliente.DataSource = clientes;
                cmbCliente.DisplayMember = "nombreCliente";
                cmbCliente.ValueMember = "iDCliente";


                var tipoDoc = db.tb_documento.ToList();
                cmbTipoDoc.DataSource = tipoDoc;
                cmbTipoDoc.DisplayMember = "nombreDocumento";
                cmbTipoDoc.ValueMember = "iDDocumento";
            }
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarCombo();
            retornoId();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmBusqueda busqueda = new FrmBusqueda();
            busqueda.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dtvVentas.Rows.Add(txtIdProdcuto.Text,txtProducto.Text,txtPrecioProducto.Text,txtCantidad.Text,txtTotal.Text);
            try
            {
                calcular();
            }
            catch (Exception ex) 
            {
            
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }


        void calcular() 
        {
            try
            {
                double precioProd;
                double cantidad;
                double total;

                precioProd = Convert.ToDouble(txtPrecioProducto.Text);
                cantidad = Convert.ToDouble(txtCantidad.Text);
                total = precioProd * cantidad;

                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {

                txtCantidad.Text = "0";
                MessageBox.Show("No se pueden operar datos menores a 0");
                txtCantidad.Select();
            }
        }
    }
}
