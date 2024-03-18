using System;
using System.Runtime.Serialization;

namespace DigitalBankWCF.Data.Models
{
    public class Usuario
    {
        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public DateTime FechaNacimiento { get; set; }

        [DataMember]
        public int IdGenero { get; set; }

        [DataMember]
        public int IdEstado { get; set; }
    }
}
