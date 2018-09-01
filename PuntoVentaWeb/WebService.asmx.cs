using CN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PuntoVentaWeb
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string nombre)
        {
            return "Hello World " + nombre ;
        }

        [WebMethod]
        public List<Producto> ListAllProducts()
        {
            return Producto.traerTodos(true);
        }

        [WebMethod]
        public Producto GetProductById(int idProducto)
        {
            return Producto.traerPorId(idProducto);
        }

        [WebMethod]
        public Producto SaveNewProduct(int idProducto, string nombre,
                        string descripcion,  decimal precioCompra,
                        decimal precioVenta,  int inventario)
        {
            Producto prodNuevo = new Producto(idProducto, nombre,
                                        descripcion, precioCompra,
                                        precioVenta, inventario);
            prodNuevo.guardar();
            return prodNuevo;

        }
    }
}
