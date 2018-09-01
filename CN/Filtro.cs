using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Filtro
    {
        public static string ValueMember = "id";
        public static string DisplayMember = "descripcion";

        public int id { get; set; }
        public string descripcion { get; set; }
        public static List<Filtro> traerTodos()
        {
            var listado = new List<Filtro>();

            var filtro1 = new Filtro() { id = 0, descripcion = "Todas"  };

            listado.Add(filtro1);
            listado.Add(statusActivo());
            listado.Add(statusCancelado());
            return listado;
        }

        public static Filtro statusActivo() => new Filtro() { id = 1, descripcion = "Activas" };

        public static Filtro statusCancelado() => new Filtro() { id = 2, descripcion = "Canceladas" };
             
    }
}
