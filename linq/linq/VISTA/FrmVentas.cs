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
            calcularTotal();
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

                txtCantidad.Text = "1";
                txtCantidad.Select();
            }
        }

        void calcularTotal() 
        {
            Double suma = 0;
            for (int i = 0; i < dtvVentas.RowCount; i++)
            {
                String datosAOperar = dtvVentas.Rows[i].Cells[4].Value.ToString();
                Double datosConvertidos = Convert.ToDouble(datosAOperar);
                suma += datosConvertidos;

                txtTotalVenta.Text = suma.ToString();
            }

            dtvVentas.Refresh();
            dtvVentas.ClearSelection();
            int ulitmaFila = dtvVentas.Rows.Count - 1;
            dtvVentas.FirstDisplayedScrollingRowIndex = ulitmaFila;
            dtvVentas.Rows[ulitmaFila].Selected = true;

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
                    Decimal precioConvertido = Convert.ToDecimal(precio);


                    String total = dtvVentas.Rows[i].Cells[4].Value.ToString();
                    Decimal totalConvertido = Convert.ToDecimal(total);


                    dete.idVenta = Convert.ToInt32(txtNumVenta.Text);
                    dete.idProducto = idProductoConvertido;
                    dete.cantidad = cantidadConvertida;
                    dete.precio = precioConvertido;
                    dete.total = totalConvertido;
                    db.detalleVenta.Add(dete);
                    db.SaveChanges();
                    txtIdProdcuto.Text = "";
                    txtProducto.Text = "";
                    txtPrecioProducto.Text = "";
                    txtTotal.Text = "";
                }
            }
            retornoId();
            dtvVentas.Rows.Clear();
            txtTotalVenta.Text = "";
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtBuscar.Text=="") 
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSearch.PerformClick();
                } else if (e.KeyCode == Keys.Enter) 
                {
                    using (sistema_ventasEntities bd = new sistema_ventasEntities()) 
                    {
                        producto prod = new producto();
                        int buscar = int.Parse(txtBuscar.Text);
                        prod = bd.producto.Where(idBuscar => idBuscar.idProducto == buscar).First();
                        txtIdProdcuto.Text = Convert.ToString(prod.idProducto);
                        txtProducto.Text = Convert.ToString(prod.nombreProducto);
                        txtPrecioProducto.Text = Convert.ToString(prod.precioProducto);
                        txtCantidad.Focus();
                        txtBuscar.Text = "";
                        intentos = 2;
                    }
                }
            }
            txtBuscar.Text = "";
        }


        int intentos = 1;
        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (intentos == 2) {
                    
                btnAgregar.PerformClick();
               
                txtIdProdcuto.Text = "";
                txtProducto.Text = "";
                txtPrecioProducto.Text = "";
                txtTotal.Text = "";
                intentos = 0;
                txtCantidad.Text = "1";
                txtBuscar.Focus();
                }
                intentos += 1;
            }
              
                
        }

        private void dtvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dtvVentas_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calcularTotal();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            dtvVentas.Rows.Remove(dtvVentas.CurrentRow);
        }
    }
}
