using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sozilan.UI.Models.Attribute
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //kullanıcı otantike değilse sayfaya yönlendirme işlemi ona göre yapılacak
            // o anki kullanıcı id si otantike değilse
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                // contorller olarak account a action olarak ta login sayfasına yönlendirdik
                //admin sayfasına yönelmek isteyen otantike değilse artık direkt oalrak login sayfasına gidecek
            }
        }
    }
}