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
    public class Cliente : ITabla
    {
        private string nombreSPU = "dbo.SPUClientes";
        private string nombreSPI = "dbo.SPIClientes";
        private static string nombreSPD = "dbo.SPDClientes";
        private static string nombreSPS = "dbo.SPSClientes";

        #region Propiedades
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string rfc { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool esActivo { get; set; }
        #endregion

        #region Constructores
        public Cliente(int idCliente, string nombre, string apPaterno,
                        string apMaterno, string rfc, string direccion,
                        string correo, string telefono)
        {
            this.idCliente = idCliente;
            this.nombre = nombre;
            this.apPaterno = apPaterno;
            this.apMaterno = apMaterno;
            this.rfc = rfc;
            this.direccion = direccion;
            this.correo = correo;
            this.telefono = telefono;
        }
        public Cliente(DataRow fila)
        {
            idCliente = fila.Field<int>("idCliente");
            nombre = fila.Field<string>("nombre");
            apPaterno = fila.Field<string>("apPaterno");
            apMaterno = fila.Field<string>("apMaterno");
            rfc = fila.Field<string>("rfc");
            direccion = fila.Field<string>("direccion");
            correo = fila.Field<string>("correo");
            telefono = fila.Field<string>("telefono");
            fechaCreacion = fila.Field<DateTime>("fechaCreacion");
            esActivo = fila.Field<bool>("esActivo");
        }
        #endregion

        #region Metodos y Funciones
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", nombre));
            parametros.Add(new SqlParameter("@apPaterno", apPaterno));
            parametros.Add(new SqlParameter("@apMaterno", apMaterno));
            parametros.Add(new SqlParameter("@rfc", rfc));
            parametros.Add(new SqlParameter("@direccion", direccion));
            parametros.Add(new SqlParameter("@correo", correo));            
            parametros.Add(new SqlParameter("@telefono", telefono));

            if (idCliente > 0)
            {
                //Update
                parametros.Add(new SqlParameter("@idCliente", idCliente));

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
            if (apPaterno.Length < 1) { mensaje += "El campo 'Paterno' es invalido. \n"; }
            if (apMaterno.Length < 1) { mensaje += "El campo 'Materno' es invalido. \n"; }
            if (rfc.Length < 1) { mensaje += "El campo 'RFC' es invalido. \n"; }
            if (direccion.Length < 1) { mensaje += "El campo 'Direccion' es invalido. \n"; }
            if (correo.Length < 1) { mensaje += "El campo 'Correo' es invalido. \n"; }
            if (telefono.Length < 1) { mensaje += "El campo 'Telefono' es invalido. \n"; }            
            return mensaje;
        }

        public static List<Cliente> traerTodos(bool soloActivos = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            //Se pregunta si se deseea traer solo Clientes activos
            if (soloActivos == true)
            {
                parametros.Add(new SqlParameter("@esActivo", true));
            }

            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            List<Cliente> listado = new List<Cliente>();

            foreach (DataRow item in dt.Rows)
            {
                Cliente fila = new Cliente(item);
                listado.Add(fila);
            }

            return listado;
        }

        public static Cliente traerPorId(int idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idCliente", idCliente));

            DataTable dt = new DataTable();

            DataBaseHelper.Fill(dt, nombreSPS, parametros.ToArray());

            Cliente resultado = null;

            if (dt.Rows.Count == 1)
            {
                resultado = new Cliente(dt.Rows[0]);
            }

            return resultado;
        }

        public static void desactivar(int idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idCliente", idCliente));

            if (DataBaseHelper.ExecuteNonQuery(nombreSPD, parametros.ToArray()) == 0)
            {
                throw new Exception(Constantes.mensajeError);
            }
        }
        #endregion
    }
}
