using CD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Producto : ITabla
    {
        private string nombreSPU = "dbo.SPUProductos";
        private string nombreSPI = "dbo.SPIProductos";
        private static string nombreSPD = "dbo.SPDProductos";
        private static string nombreSPS = "dbo.SPSProductos";

        #region Propiedades        
        public int idProducto { get; private set; }
        public string nombre { get; private set; }
        public string descripcion { get; private set; }
        public decimal precioCompra { get; private set; }
        public decimal precioVenta { get; private set; }
        public int inventario { get; private set; }
        public DateTime fechaCreacion { get; private set; }
        public bool esActivo { get; private set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor para crear/modificar un registro
        /// </summary>
        /// <param name="idProducto">identificador del producto</param>
        /// <param name="nombre">nombre del producto</param>
        /// <param name="descripcion">descripcion del producto</param>
        /// <param name="precioCompra">Precio de compra del producto</param>
        /// <param name="precioVenta">Precio de venta del producto</param>
        /// <param name="inventario">Cantidad de este producto en inventario</param>
        public Producto(int idProducto, 
                        string nombre,
                        string descripcion,
                        decimal precioCompra,
                        decimal precioVenta,
                        int inventario)
        {
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precioCompra = precioCompra;
            this.precioVenta = precioVenta;
            this.inventario = inventario;
        }

        public Producto(DataRow fila)
        {
            this.idProducto = fila.Field<int>("idProducto");
            this.nombre = fila.Field<string>("nombre");
            this.descripcion = fila.Field<string>("descripcion");
            this.precioCompra = fila.Field<decimal>("precioCompra");
            this.precioVenta = fila.Field<decimal>("precioVenta");
            this.inventario = fila.Field<int>("inventario");
            this.fechaCreacion = fila.Field<DateTime>("fechaCreacion");
            this.esActivo = fila.Field<bool>("esActivo");
        }
        #endregion

        #region Metodos y Funciones
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", nombre));
            parametros.Add(new SqlParameter("@descripcion", descripcion));
            parametros.Add(new SqlParameter("@precioCompra", precioCompra));
            parametros.Add(new SqlParameter("@precioVenta", precioVenta));
            parametros.Add(new SqlParameter("@inventario", inventario));

            if (idProducto > 0)
            {
                //Update
                parametros.Add(new SqlParameter("@idProducto", idProducto));               
                
                if (DataBaseHelper.ExecuteNonQuery(nombreSPU, parametros.ToArray()) == 0)
                {
                    //FAIL
                    throw new Exception(Constantes.mensajeError);
                }
            }
            else
            {
                //Insert
                if (DataBaseHelper.ExecuteNonQuery(nombreSPI, parametros.ToArray()) == 0)
                {
                    throw new Exception(Constantes.mensajeError);
                }
            }
        }
        public string validar()
        {
            string mensaje = "";
            if (nombre.Length < 1) { mensaje += "El campo 'Nombre' es invalido. \n"; }
            if (descripcion.Length < 1) { mensaje += "El campo 'Descripcion' es invalido. \n"; }
            if (precioCompra < 0) { mensaje += "El campo 'Precio Compra' es invalido. \n"; }
            if (precioVenta < 0) { mensaje += "El campo 'Precio Venta' es invalido. \n"; }
            if (inventario < 0) { mensaje += "El campo 'Inventario' es invalido. \n"; }
            return mensaje;
        }
        public static List<Producto> traerTodos(bool soloActivos = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            //Se pregunta si se deseea traer solo productos activos
            if (soloActivos == true)
            {
                parametros.Add(new SqlParameter("@esActivo", true));
            }

            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            List<Producto> listado = new List<Producto>();

            foreach (DataRow item in dt.Rows)
            {
                Producto fila = new Producto(item);
                listado.Add(fila);
            }

            return listado;
        }

        public static Producto traerPorId(int idProducto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idProducto", idProducto));
            
            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            Producto resultado = null;

            if (dt.Rows.Count == 1)
            {
                resultado = new Producto(dt.Rows[0]);
            }

            return resultado;
        }

        public static void desactivar(int idProducto)
        {            
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idProducto", idProducto));

            if(DataBaseHelper.ExecuteNonQuery(nombreSPD, parametros.ToArray()) == 0)
            {
                throw new Exception(Constantes.mensajeError);
            }
        }

        #endregion
    }
}
