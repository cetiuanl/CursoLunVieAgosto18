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
    public class Factura : ITabla
    {        
        private string nombreSPI = "dbo.SPIFacturas";
        private static string nombreSPC = "dbo.SPCFacturas";
        private static string nombreSPS = "dbo.SPSFacturas";

        #region Propiedades       
        public int folio { get; private set; }
        public int idCliente { get; private set; }
        public DateTime fecha { get; private set; }
        public decimal iva { get; private set; }
        public int status { get; private set; }
        public string modoPago { get; private set; }        
        #endregion

        #region Constructores
       public Factura(int idCliente, decimal iva, string modoPago)
        {
            this.idCliente = idCliente;
            this.iva = iva;
            this.modoPago = modoPago;
        }

        public Factura(DataRow fila)
        {
            this.folio = fila.Field<int>("folio");
            this.idCliente = fila.Field<int>("idCliente");
            this.fecha = fila.Field<DateTime>("fecha");
            this.iva = fila.Field<decimal>("iva");            
            this.status = fila.Field<int>("status");
            this.modoPago = fila.Field<string>("modoPago");
        }
        #endregion

        #region Metodos y Funciones
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idCliente", idCliente));
            parametros.Add(new SqlParameter("@iva", iva));
            parametros.Add(new SqlParameter("@modoPago", modoPago));
            
            if (DataBaseHelper.ExecuteNonQuery(nombreSPI, parametros.ToArray()) == 0)
            {
                throw new Exception(Constantes.mensajeError);
            }
            
        }
        public string validar()
        {
            string mensaje = "";            
            if (modoPago.Length < 1) { mensaje += "El campo 'Modo Pago' es invalido. \n"; }
            if (idCliente < 0) { mensaje += "El campo 'Cliente' es invalido. \n"; }
            if (iva < 0) { mensaje += "El campo 'IVA' es invalido. \n"; }            
            return mensaje;
        }
        public static List<Factura> traerTodos(bool soloPorStatus = false, int status = 0)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            //Se pregunta si se deseea traer solo Facturas activos
            if (soloPorStatus == true)
            {
                parametros.Add(new SqlParameter("@status", status));
            }

            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            List<Factura> listado = new List<Factura>();

            foreach (DataRow item in dt.Rows)
            {
                Factura fila = new Factura(item);
                listado.Add(fila);
            }

            return listado;
        }

        public static Factura traerPorId(int folio)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@folio", folio));

            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            Factura resultado = null;

            if (dt.Rows.Count == 1)
            {
                resultado = new Factura(dt.Rows[0]);
            }

            return resultado;
        }

        public static void desactivar(int folio, int status)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idFactura", folio));
            parametros.Add(new SqlParameter("@status", status));

            if (DataBaseHelper.ExecuteNonQuery(nombreSPC, parametros.ToArray()) == 0)
            {
                throw new Exception(Constantes.mensajeError);
            }
        }

        #endregion
    }
}
