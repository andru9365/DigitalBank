using DigitalBankWCF.Data;
using DigitalBankWCF.Data.Models;
using DigitalBankWCF.Presentation.Models;
using DigitalBankWCF.Presentation.ServiceDigitalBk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Mvc;

namespace DigitalBankWCF.Presentation.Controllers
{
    public class HomeController : Controller
    {

        Transacciones.Transacciones utilidades = new Transacciones.Transacciones();
        LoginJWT loginCredentials = new LoginJWT()
        {
            Username = ConfigurationManager.AppSettings["userToken"].ToString(),
            Password = ConfigurationManager.AppSettings["passToken"].ToString(),
        };

        List<UsuarioModel> UsuarioModelAux = new List<UsuarioModel>();
        bool errorTransaccion = false;
        string mensajeError = string.Empty;
        

        public ActionResult Index(int? page)
        {
            List<UsuarioModel> usuariosDTO = new List<UsuarioModel>();

            try
            {
                using (var client = new Service1Client())
                {
                   

                    var tokenResponse = client.TokenJWT(loginCredentials);

                    if (tokenResponse.IsAuthenticated)
                    {
                        using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                        {
                            var httpRequestProperty = new HttpRequestMessageProperty();
                            httpRequestProperty.Headers["Authorization"] = "Bearer " + tokenResponse.AccessToken;
                            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

                            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagSize"]);
                            int pageNumber = page ?? 1;

                            ServiceDigitalBk.Usuario[] usuariosArray = client.ObtenerUsuario();
                            var usuariosParaPagina = usuariosArray.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                            foreach (var usuari in usuariosParaPagina)
                            {
                                UsuarioModel usuarioDTO = new UsuarioModel
                                {
                                    IdUsuario = usuari.IdUsuario,
                                    Nombre = usuari.Nombre,
                                    FechaNacimiento = !string.IsNullOrEmpty(usuari.FechaNacimiento.ToString()) ? usuari.FechaNacimiento.ToString("yyyy-MM-dd") : string.Empty,
                                    Sexo = usuari.IdGenero,
                                    EstadoUsuario = usuari.IdEstado
                                };

                                usuariosDTO.Add(usuarioDTO);
                            }
                            UsuarioModelAux = usuariosDTO;

                            int totalUsuarios = usuariosArray.Length;
                            int totalPages = (int)Math.Ceiling((double)totalUsuarios / pageSize);

                            ViewBag.TotalPages = totalPages;
                            ViewBag.CurrentPage = pageNumber;
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error de autenticación: Las credenciales son incorrectas.";
                    }
                    errorTransaccion = false;
                }
            }
            catch (Exception ex)
            {
                errorTransaccion = true;
                mensajeError = ex.Message;
                ViewBag.ErrorMessage = "Error al obtener la información del usuario: " + ex.Message;
            }
            finally
            {
                Log log = new Log
                {
                    Metodo = ConfigurationManager.AppSettings["metodoSelectURL"].ToString(),
                    RequestMensaje = "",
                    ResponseMensaje = JsonConvert.SerializeObject(UsuarioModelAux),
                    EstadoError = errorTransaccion
                };

               utilidades.LogTransacciones(log);
            }

            return View(usuariosDTO);
        }
   



        [HttpPost]
        public ActionResult ActualizarUsuario(ServiceDigitalBk.Usuario usuario)
        {
            try
            {
                using (var client = new Service1Client())
                {

                    var tokenResponse = client.TokenJWT(loginCredentials);

                    if (tokenResponse.IsAuthenticated)
                    {
                        using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                        {
                            var httpRequestProperty = new HttpRequestMessageProperty();
                            httpRequestProperty.Headers["Authorization"] = "Bearer " + tokenResponse.AccessToken;
                            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

 
                            client.ActualizarUsuario(usuario);
                        }
                    }
                    errorTransaccion = false;
                    return Json(new { success = true });
                }

            }
            catch (Exception ex)
            {

                errorTransaccion = true;
                mensajeError = ex.Message;
                return Json(new { success = false });
            }
            finally
            {
                Log log = new Log
                {
                    Metodo = ConfigurationManager.AppSettings["metodoUpdateURL"].ToString(),
                    RequestMensaje = JsonConvert.SerializeObject(usuario),
                    ResponseMensaje = !errorTransaccion ? ConfigurationManager.AppSettings["updateOK"].ToString() : ConfigurationManager.AppSettings["updateError"].ToString() + mensajeError,
                    EstadoError = errorTransaccion
                };

                utilidades.LogTransacciones(log);
            }
        }
  
	  

        [HttpPost]
        public ActionResult EliminarUsuario(int idUsuario)
        {
            try
            {

                using (var client = new Service1Client())
                {
                    var tokenResponse = client.TokenJWT(loginCredentials);

                    if (tokenResponse.IsAuthenticated)
                    {
                        using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                        {
                            var httpRequestProperty = new HttpRequestMessageProperty();
                            httpRequestProperty.Headers["Authorization"] = "Bearer " + tokenResponse.AccessToken;
                            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;


                            client.EliminarUsuario(idUsuario);
                        }
                    }
                    errorTransaccion = false;
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                errorTransaccion = true;
                mensajeError = ex.Message;
                return Json(new { success = false });
            }
            finally
            {
                Log log = new Log
                {
                    Metodo = ConfigurationManager.AppSettings["metodoDeleteURL"].ToString(),
                    RequestMensaje = JsonConvert.SerializeObject(idUsuario),
                    ResponseMensaje = !errorTransaccion ? ConfigurationManager.AppSettings["deleteOK"].ToString() : ConfigurationManager.AppSettings["deleteError"].ToString() + mensajeError,
                    EstadoError = errorTransaccion
                };

                utilidades.LogTransacciones(log);
            }

        }
                   	

        [HttpPost]
        public ActionResult AdicionarUsuario(ServiceDigitalBk.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    using (var client = new Service1Client())
                    {


                        var tokenResponse = client.TokenJWT(loginCredentials);

                        if (tokenResponse.IsAuthenticated)
                        {
                            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                            {
                                var httpRequestProperty = new HttpRequestMessageProperty();
                                httpRequestProperty.Headers["Authorization"] = "Bearer " + tokenResponse.AccessToken;
                                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;


                                client.CrearUsuario(usuario);
                            }
                        }


                        errorTransaccion = false;
                        return Json(new { success = true });
                    }
                }
                catch (Exception ex)
                {
                    errorTransaccion = true;
                    mensajeError = ex.Message;
                    return Json(new { success = false });
                }
                finally
                {
                    Log log = new Log
                    {
                        Metodo = ConfigurationManager.AppSettings["metodoCreateURL"].ToString(),
                        RequestMensaje = JsonConvert.SerializeObject(usuario),
                        ResponseMensaje = !errorTransaccion ? ConfigurationManager.AppSettings["createOK"].ToString() : ConfigurationManager.AppSettings["createError"].ToString() + mensajeError,
                        EstadoError = errorTransaccion
                    };

                    utilidades.LogTransacciones(log);
                }
            }
            else
            {
                return Json(new { success = false, message = "Datos de usuario inválidos" });
            }
        }
    }
}
