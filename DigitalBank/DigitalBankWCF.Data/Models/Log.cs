using System;

namespace DigitalBankWCF.Data.Models
{

    public class Log
    {
        public int IdLog { get; set; }

        public DateTime FechaLog { get; set; }

        public string Metodo { get; set; }

        public string RequestMensaje { get; set; }

        public string ResponseMensaje { get; set; }

        public string IPCliente { get; set; }

        public bool EstadoError { get; set; }
    }
    
}
