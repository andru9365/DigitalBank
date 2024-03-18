using DigitalBankWCF.Data.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DigitalBankWCF.Data.CRUD
{
    public class LogDAL
    {
        private readonly ConexionBD conexion;

        public LogDAL(ConexionBD conexion)
        {
            this.conexion = conexion;
        }

        public void CrearLogTransacciones(Log log)
        {
            try
            {
                using (var conn = conexion.AbrirConexion())
                {
                    using (var cmd = new SqlCommand("SP_Adicionar_Log", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Metodo", log.Metodo);
                        cmd.Parameters.AddWithValue("@RequestMensaje", log.RequestMensaje);
                        cmd.Parameters.AddWithValue("@ResponseMensaje", log.ResponseMensaje);
                        cmd.Parameters.AddWithValue("@IPCliente", log.IPCliente);
                        cmd.Parameters.AddWithValue("@EstadoError", log.EstadoError);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear log: " + ex.Message);
                throw;
            }
        }
    }
}
