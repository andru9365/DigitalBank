using DigitalBankWCF.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DigitalBankWCF.Data.CRUD
{
    public class UsuarioDAL
    {
        private readonly ConexionBD conexion;

        public UsuarioDAL(ConexionBD conexion)
        {
            this.conexion = conexion;
        }

        public void CrearUsuario(Usuario usuario)
        {
            try
            {
                using (var conn = conexion.AbrirConexion())
                {
                    using (var cmd = new SqlCommand("SP_Adicionar_Usuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Sexo", usuario.IdGenero);
                        cmd.Parameters.AddWithValue("@EstadoUsuario", usuario.IdEstado);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear usuario: " + ex.Message);
                throw;
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using (var connection = conexion.AbrirConexion())
                {
                    using (var command = new SqlCommand("SP_Actualizar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        command.Parameters.AddWithValue("@NuevoNombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@NuevaFechaNacimiento", usuario.FechaNacimiento);
                        command.Parameters.AddWithValue("@NuevoSexo", usuario.IdGenero);
                        command.Parameters.AddWithValue("@NuevoEstado", usuario.IdEstado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar usuario: " + ex.Message);
                throw;
            }
        }

        public void EliminarUsuario(int idUsuario)
        {
            try
            {
                using (var connection = conexion.AbrirConexion())
                {
                    using (var command = new SqlCommand("SP_Eliminar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
                throw; 
            }
        }

        public List<Usuario> ConsultarUsuarios()
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();

                using (var connection = conexion.AbrirConexion())
                {
                    using (var command = new SqlCommand("SP_Consultar_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var usuario = new Data.Models.Usuario
                                {
                                    IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                    IdGenero = Convert.ToInt32(reader["Sexo"]),
                                    IdEstado = Convert.ToInt32(reader["EstadoUsuario"])
                                };

                                usuarios.Add(usuario);
                            }
                        }
                    }
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar usuarios: " + ex.Message);
                throw;
            }
        }
    }
}
