using System.Data.SqlClient;


namespace DigitalBankWCF.Data
{
    public class ConexionBD
    {
        private readonly string cadenaConexion;

        public ConexionBD(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public SqlConnection AbrirConexion()
        {
            var conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}
