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
    public class DetalleFactura : ITabla
    {
        private string nombreSPI = "dbo.SPIDetallesFactura";
        private static string nombreSPD = "dbo.SPDDetallesFactura";
        private static string nombreSPS = "dbo.SPSDetallesFactura";

        #region Propiedades       
        public int folio { get; private set; }
        public int partida { get; private set; }
        public int idProducto { get; private set; }
        public int cantidad { get; private set; }
        public decimal precioUnitario { get; private set; }
        public decimal precioImporte => cantidad * precioUnitario;
         
        #endregion

        #region Constructores
        public DetalleFactura(int folio, int idProducto,int cantidad, decimal precioUnitario)
        {
            this.folio = folio;
            this.idProducto = idProducto;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
        }

        public DetalleFactura(DataRow fila)
        {
            this.folio = fila.Field<int>("folio");
            this.partida = fila.Field<int>("partida");
            this.idProducto = fila.Field<int>("idProducto");
            this.cantidad = fila.Field<int>("cantidad");            
            this.precioUnitario = fila.Field<decimal>("precioUnitario");
        }
        #endregion

        #region Metodos y Funciones
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@folio", folio));
            parametros.Add(new SqlParameter("@idProducto", idProducto));
            parametros.Add(new SqlParameter("@cantidad", cantidad));
            parametros.Add(new SqlParameter("@precioUnitario", precioUnitario));

            if (DataBaseHelper.ExecuteNonQuery(nombreSPI, parametros.ToArray()) == 0)
            {
                throw new Exception(Constantes.mensajeError);
            }

        }
        public string validar()
        {
            string mensaje = "";
            if (idProducto < 1) { mensaje += "El campo 'Producto' es invalido. \n"; }
            if (cantidad < 1) { mensaje += "El campo 'Cantidad' es invalido. \n"; }
            if (precioUnitario < 1) { mensaje += "El campo 'Precio Unitario' es invalido. \n"; }
            return mensaje;
        }
        public static List<DetalleFactura> traerTodos(int folio)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@folio", folio));
            
            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            List<DetalleFactura> listado = new List<DetalleFactura>();

            foreach (DataRow item in dt.Rows)
            {
                DetalleFactura fila = new DetalleFactura(item);
                listado.Add(fila);
            }

            return listado;
        }

        public static DetalleFactura traerPorPartida(int folio, int partida)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@folio", folio));
            parametros.Add(new SqlParameter("@partida", partida));

            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            DetalleFactura resultado = null;

            if (dt.Rows.Count == 1)
            {
                resultado = new DetalleFactura(dt.Rows[0]);
            }

            return resultado;
        }

        public static void desactivar(int folio, int partida)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@folio", folio));
            parametros.Add(new SqlParameter("@partida", partida));

            if (DataBaseHelper.ExecuteNonQuery(nombreSPD, parametros.ToArray()) == 0)
            {
                throw new Exception(Constantes.mensajeError);
            }
        }

        #endregion
    }
}
