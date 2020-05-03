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
                txtNumVenta.Text = "1";

                foreach (var iterarDatos in tb_v) 
                {
                    int idVenta = iterarDatos.idVenta;
                    int suma = idVenta + 1;
                    txtNumVenta.Text = suma.ToString() ;
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
            try
            {
                calcular();
            }
            catch (Exception ex) 
            {
            
            }

            dtvVentas.Rows.Add(txtIdProdcuto.Text, txtProducto.Text, txtPrecioProducto.Text, txtCantidad.Text, txtTotal.Text);
            Double suma = 0;
            for (int i=0; i<dtvVentas.RowCount;i++) 
            {
                String datosAOperar = dtvVentas.Rows[i].Cells[4].Value.ToString();
                Double datosConvertidos = Convert.ToDouble(datosAOperar);
                suma += datosConvertidos;

                txtTotalVenta.Text = suma.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                tb_venta tb_v = new tb_venta();
                String comboDocumento = cmbTipoDoc.SelectedValue.ToString();
                String comboCliente = cmbCliente.SelectedValue.ToString();
                tb_v.idDocumento = Convert.ToInt32(comboDocumento);
                tb_v.iDCliente = Convert.ToInt32(comboCliente);
                tb_v.iDUsuario = 1;
                tb_v.totalVenta = Convert.ToDecimal(txtTotalVenta.Text);
                tb_v.fecha = Convert.ToDateTime(dtpFecha.Text);
                db.tb_venta.Add(tb_v);
                db.SaveChanges();

                detalleVenta dete = new detalleVenta();
                for (int i=0; i<dtvVentas.RowCount; i++) 
                {
                    String idProducto = dtvVentas.Rows[i].Cells[0].Value.ToString();
                    int idProductoConvertido = Convert.ToInt32(idProducto);


                    String cantidad = dtvVentas.Rows[i].Cells[3].Value.ToString();
                    int cantidadConvertida = Convert.ToInt32(cantidad);


                    String precio = dtvVentas.Rows[i].Cells[2].Value.ToString();
                    Decimal precioConvertido = Convert.ToInt32(precio);


                    String total = dtvVentas.Rows[i].Cells[4].Value.ToString();
                    Decimal totalConvertido = Convert.ToInt32(total);


                    dete.idVenta = Convert.ToInt32(txtNumVenta.Text);
                    dete.idProducto = idProductoConvertido;
                    dete.cantidad = cantidadConvertida;
                    dete.precio = precioConvertido;
                    dete.total = totalConvertido;
                    db.detalleVenta.Add(dete);
                    db.SaveChanges();
                }
            }
        }
    }
}
