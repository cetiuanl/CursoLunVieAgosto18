using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CN;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void TestGuardar()
        {
            bool result = false;
            string mensaje = "";

            try
            {
                Cliente nuevoCliente = new Cliente(0, "Test Nombre","Paterno","Materno",
                                    "AIQE","centro mty","@miempresa.com","01800654");
                nuevoCliente.guardar();
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

            List<Cliente> clientes = null;
            try
            {
                clientes = Cliente.traerTodos(false);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            }

            Assert.IsTrue((clientes.Count > 0), mensaje);
        }

        [TestMethod]
        public void TestTraerPorId()
        {
            int idCliente = 7;
            string mensaje = "";

            Cliente cliente = null;
            try
            {
                cliente = Cliente.traerPorId(idCliente);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            }
            Assert.IsNotNull(cliente, mensaje);            
        }
    }
}
