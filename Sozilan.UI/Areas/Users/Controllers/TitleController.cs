using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;
using Sozilan.UI.Areas.Users.Enums;
using Sozilan.UI.Areas.Users.Helpers;
using Sozilan.UI.Areas.Users.Models.ViewModel;

namespace Sozilan.UI.Areas.Users.Controllers
{
    public class TitleController : Controller
    {
        ServicePoint point;

        public TitleController()
        {
            point = new ServicePoint();
        }
        // GET: Users/Title
        public ActionResult Index()
        {
            return View();
        }

        // Get
        public ActionResult BaslikAc()
        {
            //HttpContext.User.Identity.Name.ToString() == ""
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                SetTitleTag();
                return View();
            }
        }

        [HttpPost]
        public ActionResult BaslikAc(CreateTitle ct)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            SetTitleTag();

            if (!ModelState.IsValid) 
            {
                return View(ct);
            }

            var name = HttpContext.User.Identity.Name;
            int id = point.UserService.GetID(name);

            Title tit = new Title
            {
                Content=ct._content.ToLower(),
                ContentDate=DateTime.Now,
                UpdateDate=DateTime.Now,
                UserID=id,
                IsActive=true,
                TitleTag="#"+ct._titleTag,
                TitleLink=ct._titleLink,
                IPAddress=HelperClass.GetIpHelper()
            };
            MessageService mes = point.TitleService.Insert(tit);

            Entry ent = new Entry
            {
                Content=ct._entryContent.ToLower(),
                EntryDate=DateTime.Now,
                UpCount=0,
                DownCount=0,
                TitleID=tit.ID,
                UserID=id,
                IsActive=true,
                EntryUpdateDate=DateTime.Now,
                isUpdated=false,
                IPAddress=HelperClass.GetIpHelper()
            };
            MessageService mess = point.EntryService.Insert(ent);

            return RedirectToAction("Index", "Home");
        }

        private void SetTitleTag(object titleTag=null)
        {
            string[] tt = Enum.GetNames(typeof(TitleTag));

            IList<string> enumList = tt.ToList();

            var selectList = new SelectList(enumList, titleTag);
            ViewData.Add("_titleTag", selectList); // başlıkaç.cshtml de dropdownlist te => _titleTag  yazdığımız yere bu bilgi gönderilecek
        }
    }
}