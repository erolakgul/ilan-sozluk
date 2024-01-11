using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozilan.UI.Areas.Users.Helpers
{
    public static class HelperClass
    {
        public static string GetIpHelper()
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            return ip;
        }
    }
}