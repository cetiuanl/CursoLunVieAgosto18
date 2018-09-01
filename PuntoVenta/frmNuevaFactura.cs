using CN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class frmNuevaFactura : Form
    {
        public frmNuevaFactura()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void frmNuevaFactura_Load(object sender, EventArgs e)
        {
            cboClientes.DataSource = Cliente.traerTodos(true);
            cboClientes.ValueMember = "idCliente";
            cboClientes.DisplayMember = "rfc";
        }
        private void cargarDatos()
        {
            try
            {
                int folio = IntegerExtensions.ParseInt(txtFolio.Text);
                dgvListado.DataSource = DetalleFactura.traerTodos(folio);
                dgvListado.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIdProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                int idProducto = IntegerExtensions.ParseInt(txtIdProducto.Text);

                try
                {
                    Producto prod = Producto.traerPorId(idProducto);
                    if (prod != null)
                    {
                        int folio = IntegerExtensions.ParseInt(txtFolio.Text);

                        if (folio == 0)
                        {
                            Cliente client = (Cliente)cboClientes.SelectedItem;                            
                            int iva = IntegerExtensions.ParseInt(txtIVA.Text);
                            string modoPago = txtModoPago.Text;
                            var nuevaFac = new Factura(client.idCliente, iva, modoPago);
                            txtFolio.Text = nuevaFac.guardar().ToString();
                        }

                        int cantidad = IntegerExtensions.ParseInt(txtCantidad.Text);
                        var detalle = new DetalleFactura(folio,
                                                prod.idProducto, 
                                                    cantidad, 
                                                    prod.precioVenta);

                        detalle.guardar();
                        cargarDatos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
