using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CN;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class ProductoTest
    {
        [TestMethod]
        public void TestGuardar()
        {
            bool result = false;
            string mensaje = "";

            try
            {
                Producto nuevoProd = new Producto(0, "Test Nombre", "Test Descripcion", 100, 120, 50);
                nuevoProd.guardar();
                result = true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            }

            Assert.IsTrue(result, mensaje);                        
        }

        [TestMethod]
        public void TestTraerTodos()
        {
            string mensaje = "";

            List<Producto> productos = null;
            try
            {
                productos = Producto.traerTodos(false);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            }

            Assert.IsTrue((productos.Count > 0), mensaje);
        }
    }
}
