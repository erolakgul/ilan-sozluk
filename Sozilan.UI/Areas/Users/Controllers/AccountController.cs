using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;
using Sozilan.UI.Areas.Users.Helpers;
using Sozilan.UI.Areas.Users.Models.ViewModel;
using Sozilan.UI.Models.Service;

namespace Sozilan.UI.Areas.Users.Controllers
{
    public class AccountController : Controller
    {
        ServicePoint point;

        public AccountController()
        {
            point = new ServicePoint();
        }
        // GET: Admin/Account
        public ActionResult Login()
        {
            //dışardan şifresin gelenlerin içeri girememesi,içerdekilerinde login ekranına geri dönmemesi
            //için bir kontorl mekanızması oluşturuyoruz
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //httpcontext sınıfı sayesinde session açanın oturumunun devam edip etmediğini çıkarabilirz
                return RedirectToAction("Index", "Home"); //authentice olmuşsa index e gider
                //bu şekilde içerde kalıyor
            }
            //aksi durumda ise login sayfasna yönlendiriyor oldu
            return View();
        } 
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //if (ModelState.IsValid)
            //{
                if (UserManagement.hasAccount(model))
                {
                    //user dan true dönerse session açıyoruz buradan
                    //dolayısıyla homecontroller a yönlendiriyoruz
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    //true yaparsak cookie bazlı olur,şuan session bazlı tutulacak
                    //ayrıca cookie de kalırsa browser temizlenene kadar oturum açık kalır
                    //session bazlı da belli bi timeout süresi verbiliriz
                    return RedirectToAction("Index", "Home");
                }
            //}

            return View("Login"); //valid değilse login e get isteğinde bulnyoruz
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            //çıkış yaptırdıktan sonra direk olarak login sayfasına geri gönderiyoruz
            return RedirectToAction("Login", "Account", new { area = "Users" });
        }
        //get
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User us)
        {
            if (!ModelState.IsValid)
            {
                return View(us);
            }
             
            Sozilan.Data.Model.User _user = new User
            {
                UserName=us.UserName,
                Email=us.Email,
                Password=us.Password,
                ConfirmPassword=us.ConfirmPassword,
                CreateDate=DateTime.Now,
                Role=1000,
                IsActive=false,
                UserGUID=Guid.NewGuid().ToString(),
                IPAddress=HelperClass.GetIpHelper()
            };
            MessageService mes = point.UserService.Insert(_user);

            if (mes.ResultID > 0) 
            {
                SendConfirmationEmailAddress(us.Email);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("","Kayıt işlemi başarısız..");
            }
            return View(us);
        }
        public void SendConfirmationEmailAddress(string email)
        {
            string confirmationGuid = point.UserService.GetGUID(email);

            var client = new System.Net.Mail.SmtpClient();

            var sentMail = client.Credentials.GetCredential("smtp.gmail.com", 587, "Ssl").UserName;

            string verfyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Users/Account/Verify?Id=" + confirmationGuid;

            string bodyMesaj = string.Format("İlan-ı Sözlük için üyeliğiniz oluşturulmuştur,aktif hale getirmek için aşağıdaki linke tıklayınız\n\n");
            bodyMesaj += verfyUrl;

            var mesaj = new System.Net.Mail.MailMessage(sentMail,email)
            {
                Subject="İlan-ı Sözlük'ten mailiniz var..",
                Body=bodyMesaj
            };

            client.Send(mesaj);
        }

        public ActionResult Verify(string id)
        {
            bool tiklamisMi = point.UserService.ConfirmGuid(id);

            if (!tiklamisMi)
            {
                return View();
            }
            //return RedirectToAction("Index");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ReminderMe(string str)
        {
            string[] veri = point.UserService.ParolaUnuttum(str);

            if (veri != null) 
            {
                string kullanici = veri[0];
                string sifre = veri[1];
                string email = veri[2];

                var client = new System.Net.Mail.SmtpClient();

                var sentMail = client.Credentials.GetCredential("smtp.gmail.com", 587, "Ssl").UserName;

                string msj = string.Format("Merhaba " + kullanici + ",\n" + "Kullanıcı adınız ve şifreniz şu şekildedir ;\n\n");
                string bodyMesaj = string.Format("Kullanıcı Adınız  : " + kullanici + "\nŞifreniz                : " + sifre);
                string uyari = string.Format("\n\n\n\nEğer bu mail size bilginiz dışında ulaştıysa bir yerlerde oturumunuzu açık bırakmış olabilirsiniz.\nŞifrenizi kendinizin değiştirmesini tavsiye ederiz..");

                var mesaj = new System.Net.Mail.MailMessage(sentMail, email)
                {
                    Subject = "İlan-ı Sözlük Parolamı Hatırlatma..",
                    Body = msj + bodyMesaj + uyari
                };
                client.Send(mesaj);

                return RedirectToAction("Login");
            }
            return new JavaScriptResult { Script = "alert('kullanıcı adı geçerli değil')" };
        }

    }
}