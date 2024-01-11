using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;
using Sozilan.UI.Areas.Users.Helpers;
using Sozilan.UI.Models.Attribute;

namespace Sozilan.UI.Areas.Users.Controllers
{
    public class MessageController : Controller
    {
        ServicePoint point;
        public MessageController()
        {
            point = new ServicePoint();
        }

        // GET: Users/Message
        [Login]
        public ActionResult Index()
        {
            //mesajların okundu bilgisi ile isactive=false yapılıyor

            point.MesajService.Okundu(HttpContext.User.Identity.Name.ToString());

            int userId = point.UserService.GetID(HttpContext.User.Identity.Name.ToString());

            var list = point.MesajService.GetMessages(userId);

            return View(list);
            //return View();
        }

        [Login]
        [HttpPost]
        public ActionResult MesajGonder(string msg)
        {
            string[] gonderilen = msg.Split('-');

            string gonderenİsim = gonderilen[0];
            string gonderilenİsim = gonderilen[1];

            string mesaj = "";

            for (int i = 2; i < gonderilen.Length; i++)
            {
                 mesaj = mesaj +"-"+  gonderilen[i];
            }
            mesaj = mesaj.Substring(1);

            if (mesaj.Length == 0)
            {
                return View();
            }

            int gonderenID = point.UserService.GetID(gonderenİsim);
            int gonderilenID = point.UserService.GetID(gonderilenİsim);
            //kayıtvarmı methoduna buradan id leri gönder guid yi bi çek sonra insert olayına bakarsın
            string g = point.MesajService.KayıtVarMi(gonderenID, gonderilenID);

            if (g == "boş") // konuşma hiç başlatılmamışsa
            {
                string gu=Convert.ToString(Guid.NewGuid());
                Message msj1 = new Message
                {
                    Content = mesaj,
                    SendDate = DateTime.Now,
                    UserID = gonderenID,
                    IPAddress = HelperClass.GetIpHelper(),
                    ReceivingID = gonderilenID,
                    Receiving = gonderilenİsim,
                    MesajGUID= gu
                };
                var gonderildi = point.MesajService.Insert(msj1);
            }
            else
            {
                Message msj = new Message
                {
                    Content = mesaj,
                    SendDate = DateTime.Now,
                    UserID = gonderenID,
                    IPAddress = HelperClass.GetIpHelper(),
                    ReceivingID = gonderilenID,
                    Receiving = gonderilenİsim,
                    MesajGUID=g
                };
                var gonderildi = point.MesajService.Insert(msj);
            }

            var listMesaj = point.MesajService.GetMessage(gonderenID,gonderilenID);

            return PartialView("MesajlarPartial", listMesaj);
        }

        [HttpPost]
        public ActionResult MesajListele(string str)
        {
            var list = point.MesajService.GetMessage(str);
            return PartialView("MesajListelePartial", list);
        }

        [HttpPost]
        public ActionResult MesajVarMi()
        {
            int userId = point.UserService.GetID(HttpContext.User.Identity.Name.ToString());

            var list = point.MesajService.GetMessage(userId);

            return PartialView("MesajlarPartial", list);
        }

        [HttpPost]
        public ActionResult CevapYaz(Message msg)
        {
            int userId = point.UserService.GetID(HttpContext.User.Identity.Name.ToString());
            
            if (userId == msg.UserID)
            {
                Message msj = new Message
                {
                    Content = msg.Content,
                    SendDate = DateTime.Now,
                    UserID = msg.UserID,
                    IsActive = true,
                    IPAddress = HelperClass.GetIpHelper(),
                    ReceivingID = msg.ReceivingID,
                    Receiving = msg.Receiving,
                    MesajGUID = msg.MesajGUID
                };
                if (msj.Content.Length == 0) //boş mesaj istemiyoruz
                {
                    return RedirectToAction("Index", "Message");
                }
                MessageService msf = point.MesajService.Insert(msj);
            }
            else
            {
                string username = point.UserService.GetName(msg.UserID.Value);

                Message msj = new Message
                {
                    Content = msg.Content,
                    SendDate = DateTime.Now,
                    UserID = msg.ReceivingID,
                    IsActive = true,
                    IPAddress = HelperClass.GetIpHelper(),
                    ReceivingID = msg.UserID.Value,
                    Receiving = username,
                    MesajGUID = msg.MesajGUID
                };
                if (msj.Content.Length == 0)//boş mesaj istemiyoruz
                {
                    return RedirectToAction("Index", "Message");
                }
                MessageService msf = point.MesajService.Insert(msj);
            }

            return RedirectToAction("Index", "Message");
        }

        [HttpPost]
        public ActionResult BildirimBegenme(int? entryID)
        {
            var _userName = HttpContext.User.Identity.Name.ToString();
            int? titleID=point.EntryService.GetTitleID(entryID.Value);
            int? bildirilenID = point.EntryService.GetUser(entryID.Value);
            string bildirilenKullanici = point.EntryService.GetUserName(entryID.Value);

            if (_userName == bildirilenKullanici) // kişi kendi yorumunu beğenmek isterse,geri gönderiyoruz.Seni çakaall :)
            {
                return View();
            }

            Notification nt = new Notification
            {
                UserName=_userName,
                EventID=0,
                EventDate=DateTime.Now,
                IsActive=true,
                IPAddress=HelperClass.GetIpHelper(),
                EntryID=entryID.Value,
                TitleID=titleID.Value,
                BildirilenID=bildirilenID.Value
            };
            MessageService msg = point.NotificationService.Insert(nt);

            var list = point.NotificationService.GetList(nt.BildirilenID);

            return PartialView("BildirimPartial", list);
        }

        [HttpPost]
        public ActionResult BildirimOkundu()
        {
            var _userName = HttpContext.User.Identity.Name.ToString();
            var id = point.UserService.GetID(_userName);

            Notification nt = new Notification
            {
                BildirilenID=id
            };

            MessageService msg = point.NotificationService.Update(nt);

            return View();
        }

        [HttpPost]
        public ActionResult BildirimVarMi()
        {
            int userId = point.UserService.GetID(HttpContext.User.Identity.Name.ToString());

            var list = point.NotificationService.GetList(userId);

            return PartialView("BildirimPartial", list);
        }
    }
}