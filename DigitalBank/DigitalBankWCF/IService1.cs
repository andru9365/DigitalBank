using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using DigitalBankWCF.Data.Models;
using DigitalBankWCF.Request;
using DigitalBankWCF.Response;
using static DigitalBankWCF.Service1;

namespace DigitalBankWCF
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Usuarios")]
        void CrearUsuario(Usuario usuario);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Usuarios/{id}")]
        List<Usuario> ObtenerUsuario();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Usuarios/{id}")]
        void ActualizarUsuario(Usuario usuario);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Usuarios/{id}")]
        void EliminarUsuario(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GenerateToken")]
        TokenResponse TokenJWT(LoginJWT credentials);
    }
}
