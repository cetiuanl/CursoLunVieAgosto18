using CN;
using System;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void cargarDatos()
        {
            try
            {
                dgvListado.DataSource = Producto.traerTodos(true);
                dgvListado.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void limpiarDatos()
        {

        }

        public Producto productoActual
        {
            get
            {
                int idProducto = IntegerExtensions.ParseInt(txtIdProducto.Text);
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                decimal precioCompra = DecimalExtensions.ParseDecimal(txtPrecioCompra.Text);
                decimal precioVenta = DecimalExtensions.ParseDecimal(txtPrecioVenta.Text);
                int inventario = IntegerExtensions.ParseInt(txtInventario.Text);

                return new Producto(idProducto, nombre, descripcion, precioCompra, precioVenta, inventario);                
            }
            set
            {
                Producto nuevoProducto = (Producto)value;

                txtIdProducto.Text = nuevoProducto.idProducto.ToString();
                txtNombre.Text = nuevoProducto.nombre;
                txtDescripcion.Text = nuevoProducto.descripcion;
                txtPrecioCompra.Text = nuevoProducto.precioCompra.ToString();
                txtPrecioVenta.Text = nuevoProducto.precioVenta.ToString();
                txtInventario.Text = nuevoProducto.inventario.ToString();
            }
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                productoActual.guardar();
                MessageBox.Show(Constantes.mensajeOK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cargarDatos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Producto productoLimpio = new Producto(0, "", "", 0, 0, 0);
            productoActual = productoLimpio;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto.desactivar(productoActual.idProducto);                
            }
            catch (Exception ex)
            {
                string mensaje = $"{ex.Message} Contacta al Administrador";
                MessageBox.Show(mensaje, "Ha ocurrido un error",  MessageBoxButtons.OK,  MessageBoxIcon.Error);
            }
            cargarDatos();
        }

        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListado.CurrentRow.DataBoundItem.GetType() == typeof(Producto))
            {
                productoActual = (Producto)dgvListado.CurrentRow.DataBoundItem;
            }
        }
    }
}
