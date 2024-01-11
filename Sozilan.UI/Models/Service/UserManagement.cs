using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sozilan.UI.Areas.Users.Models.ViewModel;
using Sozilan.Data.DataContext;

namespace Sozilan.UI.Models.Service
{
    public class UserManagement
    {
        public static bool hasAccount(LoginModel model)
        {


            using (ManagementSystemContext db=new ManagementSystemContext())
            {
                //silinmemiş bir kullanıcı ise username ve passwordler tutuyorsa
                return db.User.Any(x => x.IsActive && x.UserName == model.UserName && x.Password == model.Password);
            }
        }
    }
}