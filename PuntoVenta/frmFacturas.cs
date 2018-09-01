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
    public partial class frmFacturas : Form
    {
        public frmFacturas()
        {
            InitializeComponent();
        }

        private void frmFacturas_Load(object sender, EventArgs e)
        {
            cargarDatos(false, 0);

            cboFiltrar.DataSource = Filtro.traerTodos();
            cboFiltrar.ValueMember = Filtro.ValueMember;
            cboFiltrar.DisplayMember = Filtro.DisplayMember;
            cboFiltrar.Refresh();
        }

        private void cargarDatos(bool soloPorStatus, int status)
        {
            dgvListado.DataSource = Factura.traerTodos(soloPorStatus, status);
            dgvListado.Refresh();   
        }

        private void cboFiltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtro filtroActual = (Filtro)cboFiltrar.SelectedItem;
            if (filtroActual.id != 0)
            {
                cargarDatos(true, filtroActual.id);
            }
            else
            {
                cargarDatos(false, 0);
            }                        
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var form = new frmNuevaFactura();
            form.Show();
        }
    }
}
