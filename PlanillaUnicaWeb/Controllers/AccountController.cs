using PlanillaUnicaWeb.Models;
using PlanillaUnicaWeb.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlanillaUnicaWeb.Controllers
{
    public class AccountController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> SignIn(string email, string password)
        {
            HttpResponseMessage response;
            string contents;
            var httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {

                //int rutEntero = Convert.ToInt32(rut.Substring(0, rut.Length - 2).Replace(".", ""));

                //Realizamos la llamada a la API para validar el usuario
                //Rescatamos el usuario que esta conectado
                Account_Usuario usuario = new Account_Usuario();
                Account_Login login = new Account_Login();
                //var json = await httpClient.GetStringAsync(Resource.UsuarioLogin + rutEntero.ToString() + "/" + password);
                login.Email = email;
                login.Password = password;

                var json = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["ApiAccount"] + Resource.Account_LoginUsuario, login);

                if (json.IsSuccessStatusCode)
                {
                    var resultado = json.Content.ReadAsAsync<Account_Estatus_Login>().Result;
                    //EstatusLogin estadoLogin = JsonConvert.DeserializeObject<EstatusLogin>(resultado.ToString());

                    if (resultado.Codigo.Equals("ok"))
                    {
                        var json_usuario = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiAccount"] + Resource.Account_UsuarioMail + "?email=" + login.Email.ToUpper().Trim());
                        usuario = JsonConvert.DeserializeObject<Account_Usuario>(json_usuario);
                        if (!string.IsNullOrEmpty(usuario.Accusu_Nombre))
                        {
                            Session["user_Estado"] = usuario.Accestlog_Id;

                            if (usuario.Accestlog_Id.Equals(2))
                            {
                                Session["user_Id"] = usuario.Accusu_Id;
                                return RedirectToAction("Index", "CambioClave");
                            }
                            else
                            {
                                Session["user_Name"] = usuario.Accusu_Nombre;
                                Session["user_ApellidoPaterno"] = usuario.Accusu_Apel_Pater;
                                Session["user_ApellidoMaterno"] = usuario.Accusu_Apel_Mater;
                                Session["user_Id"] = usuario.Accusu_Id;
                                Session["user_Email"] = usuario.Accusu_Email;
                                Session["user_Area"] = usuario.Genara_Id;

                                //VALIDAR SI TIENE O NO ACCESO AL SISTEMA QUE SE ESTA VALIDANDO (2)
                                Account_Usuario_Sistema usuarioSistema = new Account_Usuario_Sistema();
                                usuarioSistema.Accsis_Id = Convert.ToDecimal(ConfigurationManager.AppSettings["SistemaId"]);
                                usuarioSistema.Accusu_Id = usuario.Accusu_Id;
                                response = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["ApiAccount"] + Resource.Account_Acceso + "/ValidaAccesoSistema", usuarioSistema);
                                contents = await response.Content.ReadAsStringAsync();
                                Resultado resultadoExe = JsonConvert.DeserializeObject<Resultado>(contents);
                                if (resultadoExe.Id > 0) //Si tiene perfil vigente para el sistema
                                {
                                    //Rescatamos el menú del usuario de acuerdo a su perfil 
                                    var jsonMenus = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiAccount"] + Resource.Account_Acceso + "/GetMenuAsociadosPerfil?sistemaId=" + ConfigurationManager.AppSettings["SistemaId"] + "&perfilId=" + resultadoExe.Id);
                                    List<Account_Menu_Opcion> usuarioMenus = JsonConvert.DeserializeObject<List<Account_Menu_Opcion>>(jsonMenus);
                                    Session["user_Menus"] = usuarioMenus;
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    Session["user_Name"] = null;
                                    TempData["Mensaje"] = "El usuario no tiene acceso al sistema.";
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                        else
                        {
                            TempData["Mensaje"] = "Error al autenticar usuario.";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["Mensaje"] = resultado.Descripcion;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Mensaje"] = "Error al autenticar usuario.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Mensaje"] = "Error al autenticar usuario.";
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}