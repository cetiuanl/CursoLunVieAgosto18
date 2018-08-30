using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    interface ITabla
    {
        /// <summary>
        /// Funcion utilizada para ejecutar un SP de Insert o Updates
        /// hacia la base de datos
        /// </summary>
        void guardar();
        /// <summary>
        /// Funcion utilizada para devolver un mensaje de error
        /// si una propiedad en el objeto es erronea.
        /// </summary>
        /// <returns></returns>
        string validar();
    }
}
