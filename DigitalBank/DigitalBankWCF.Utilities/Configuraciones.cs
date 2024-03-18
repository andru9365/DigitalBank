using System.Configuration;

namespace DigitalBankWCF.Utilities
{
    public class Configuraciones
    {
        public static string CadenaConexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
            }
        }

        public static string usuariosJWT
        {
            get
            {
                return ConfigurationManager.AppSettings["usuariosJWT"].ToString();
            }
        }


        public static string passJWT
        {
            get
            {
                return ConfigurationManager.AppSettings["passJWT"].ToString();
            }
        }

        public static string secretKeyJWT
        {
            get
            {
                return ConfigurationManager.AppSettings["secretKeyJWT"].ToString();
            }
        }

    }
}
