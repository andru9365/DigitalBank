using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalBankWCF.Presentation.Models
{
   
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string FechaNacimiento { get; set; }
        public int Sexo { get; set; }
        public int EstadoUsuario { get; set; }

        public UsuarioModel()
        {
        }
    }

}