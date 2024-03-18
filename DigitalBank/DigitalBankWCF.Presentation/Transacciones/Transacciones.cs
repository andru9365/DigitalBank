using DigitalBankWCF.Data;
using DigitalBankWCF.Data.CRUD;
using DigitalBankWCF.Utilities;
using System;
using System.Net;

namespace DigitalBankWCF.Presentation.Transacciones
{
    public class Transacciones
    {
        private readonly LogDAL logDAL;

        public Transacciones()
        {
            ConexionBD conexion = new ConexionBD(Configuraciones.CadenaConexion);
            logDAL = new LogDAL(conexion);
        }
        public void LogTransacciones(Data.Models.Log logs)
        {
            try
            {
                string ipCliente = ObtenerIPCliente();

                Data.Models.Log log = new Data.Models.Log
                {
                    Metodo = logs.Metodo,
                    RequestMensaje = logs.RequestMensaje,
                    ResponseMensaje = logs.ResponseMensaje,
                    IPCliente = ipCliente,
                    EstadoError = logs.EstadoError,
                };

                logDAL.CrearLogTransacciones(log);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al llamar al método CrearLog: " + ex.Message);
            }
        }

        private string ObtenerIPCliente()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }
            return null;
        }
    }
}