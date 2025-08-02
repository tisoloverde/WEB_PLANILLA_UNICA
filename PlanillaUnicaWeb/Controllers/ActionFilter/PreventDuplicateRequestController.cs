using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Web.Caching;
using System.Web.Mvc;

namespace PlanillaUnicaWeb.Controllers.ActionFilter
{
    public class PreventDuplicateRequestController : ActionFilterAttribute
    {
        //This stores the time between Requests (in seconds)
        public int DelayRequest = 1800;
        //The Error Message that will be displayed in case of excessive Requests
        public string ErrorMessage = "No es posible realizar multiples solicitudes.";
        //This will store the URL to Redirect errors to
        public string RedirectURL;

        public PreventDuplicateRequestController()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var ip = string.Empty;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var itemIp in host.AddressList)
                {
                    if (itemIp.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ip = itemIp.ToString();
                    }
                }

                var cookies = filterContext.HttpContext.Request.ServerVariables["HTTP_COOKIE"].ToString().Split(';')[0];

                var request = filterContext.HttpContext.Request;
                var cache = filterContext.HttpContext.Cache;
                var originationInfo = ip + request.UserHostAddress + cookies;
                originationInfo += request.UserAgent;
                var targetInfo = request.RawUrl + request.QueryString;
                var hashValue = BCrypt.Net.BCrypt.HashPassword(originationInfo + targetInfo);



                bool existeCache = false;

                IDictionaryEnumerator allCaches = cache.GetEnumerator();

                while (allCaches.MoveNext())
                {

                    var cache_ = filterContext.HttpContext.Cache.Get(allCaches.Value.ToString());

                    if (cache_ != null)
                    {
                        if (BCrypt.Net.BCrypt.Verify(originationInfo + targetInfo, cache_.ToString()))
                        {
                            filterContext.Controller.ViewData.ModelState.AddModelError("ExcessiveRequests", ErrorMessage);
                            existeCache = true;
                            break;
                        }


                    }
                }

                if (!existeCache)
                {
                    cache.Add(hashValue, hashValue, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}