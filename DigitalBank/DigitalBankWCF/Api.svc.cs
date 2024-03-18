using DigitalBankWCF.Data;
using DigitalBankWCF.Data.CRUD;
using DigitalBankWCF.Data.Models;
using DigitalBankWCF.JWT;
using DigitalBankWCF.Request;
using DigitalBankWCF.Response;
using DigitalBankWCF.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.ServiceModel.Web;

namespace DigitalBankWCF
{

    public class Service1 : IService1
    {
        private readonly UsuarioDAL usuarioCRUD;

        public Service1()
        {
            ConexionBD conexion = new ConexionBD(Configuraciones.CadenaConexion);
            usuarioCRUD = new UsuarioDAL(conexion);
        }

        public Service1(string cadenaConexion) : this()
        {
            ConexionBD conexion = new ConexionBD(cadenaConexion);
            usuarioCRUD = new UsuarioDAL(conexion);
        }

        [WebInvoke(Method = "POST", UriTemplate = "Usuarios")]
        public void CrearUsuario(Data.Models.Usuario usuario)
        {
            if (GenerarToken.Authenticate().IsAuthenticated)
                usuarioCRUD.CrearUsuario(usuario);
            else
                throw new SecurityTokenValidationException("Unauthorized");
        }

        [WebInvoke(Method = "GET", UriTemplate = "Usuarios/{id}")]
        public List<Usuario> ObtenerUsuario()
        {
            if (GenerarToken.Authenticate().IsAuthenticated)
                return usuarioCRUD.ConsultarUsuarios();
            else
                throw new SecurityTokenValidationException("Unauthorized");
        }

        [WebInvoke(Method = "PUT", UriTemplate = "Usuarios/{id}")]
        public void ActualizarUsuario(Usuario usuario)
        {
            if (GenerarToken.Authenticate().IsAuthenticated)
                usuarioCRUD.ActualizarUsuario(usuario);
            else
                throw new SecurityTokenValidationException("Unauthorized");
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "Usuarios/{id}")]
        public void EliminarUsuario(int id)
        {
            if (GenerarToken.Authenticate().IsAuthenticated)
                usuarioCRUD.EliminarUsuario(id);
            else
                throw new SecurityTokenValidationException("Unauthorized");
        }

        public TokenResponse TokenJWT(LoginJWT credentials)
        {
            if (credentials.Username == Configuraciones.usuariosJWT && credentials.Password == Configuraciones.passJWT)
            {
                var token = GenerarToken.GenerateToken(credentials);

                return new TokenResponse
                {
                    AccessToken = token,
                    IsAuthenticated =true
                };
            }
            else
            {
                return new TokenResponse
                {
                    AccessToken = null,
                    IsAuthenticated = false
                };
            }
        }
    }
}
