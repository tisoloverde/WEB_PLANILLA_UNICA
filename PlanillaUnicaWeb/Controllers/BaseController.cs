using PlanillaUnicaWeb.Models;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PlanillaUnicaWeb.Controllers
{
    public class BaseController : Controller
    {
        protected void Flash(string message, string debug = null)
        {
            var alerts = TempData.ContainsKey(Alert.AlertKey) ?
                (List<Alert>)TempData[Alert.AlertKey] :
                new List<Alert>();

            alerts.Add(new Alert
            {
                Message = message,
                Debug = debug
            });

            TempData[Alert.AlertKey] = alerts;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.HttpContext.Request.FilePath.Equals("/")) && !(filterContext.HttpContext.Request.FilePath.Equals("/Account/SignIn")))
            {
                if (!(Session["user_Name"] == null))
                {
                    ViewBag.MessageSesion = "En Sesion";
                }
                else if (!(Session["user_Estado"] == null) && Session["user_Estado"].ToString() == "2")
                {
                    // The session has lost data. This happens often
                    // when debugging. Log out so the user can log back in

                    Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                    filterContext.Result = RedirectToAction("Index", "CambioClave");
                }
                else
                {
                    // The session has lost data. This happens often
                    // when debugging. Log out so the user can log back in
                    Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                    filterContext.Result = RedirectToAction("Index", "Home");
                }
            }
        }
    }
}