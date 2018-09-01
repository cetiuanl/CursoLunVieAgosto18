using CN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PuntoVentaWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dgvListado.DataSource = Producto.traerTodos(true);
            dgvListado.DataBind();
        }
    }
}