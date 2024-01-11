using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;
using Sozilan.UI.Areas.Users.Models.ViewModel;
using Sozilan.UI.Models.Attribute;
using PagedList;
using PagedList.Mvc;
using Sozilan.UI.Areas.Users.Helpers;

namespace Sozilan.UI.Areas.Users.Controllers
{
    public class HomeController : Controller
    {
        ServicePoint point;

        public HomeController()
        {
            point = new ServicePoint();
        }
        // GET: Users/Home
        [Login] //Users in içine girmek için şifre girilmesini zorunlu hale getiriyoruz
        public ActionResult Index()
        {
            string ip = Request.UserHostAddress;
            
            int sonId = point.TitleService.LastID(); //başlıkların son id sinin nosu nu çekiyorz

            Random r = new Random();
            List<int> sayiDizi = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                int rSayi = r.Next(1, sonId);
                if (sayiDizi.IndexOf(rSayi) != -1)
                {
                    i--;
                    continue;
                }
                else
                {
                    sayiDizi.Add(rSayi);  // db deki son id no suna göre 3 id no su çektik    
                }
            }

            var dizi = new int[3];
            int ba = 0;
            foreach (int items in sayiDizi)
            {
                dizi[ba] = items;
                ba++;
            }
            var kol = point.TitleService.RandomTitle(dizi);
            return View(kol);
        }

        [Login]
        public ActionResult Basliklar(int? id)
        {
            if (id == null || id == 0)
            {
                id = 1;
            }
            //var title = point.TitleService.FindByID(id.Value); // başlığın adı dönüyor buradan
            ////List<Title> entList = new List<Title>();
            ////entList.Add(title);

            // örneğin id 1 geldi,sonuç = 1 nolu başlığa ait tüm entryler
            ICollection<Entry> el = point.EntryService.GetEntryList(id.Value); //başlığa ait entryler

            if (el.Count <= 5)
            {
                // 5 ten az ise entry sayısı olduğu kadar ı gösterilecek
                var little = el.OrderBy(x => x.ID).ToPagedList(1, el.Count);
                return View(little);
            }
            var list = el.OrderBy(x => x.ID).ToPagedList(1,5); // ilk sayfa açılsın,5er 5er göstersin anlamında
            return View(list);
        }

        public ActionResult BasliklarPaging()
        {
            // başlık içindeki entry sayısına göre sayfa yönlendrmesi yapılması için eklendi
            return View();
        }

        [HttpGet]
        public ActionResult BasliklarPaging(string id)
        {
            // hem title id hem paging id gelmesi gerekiyor
            string[] gonderilenVeri = id.Split('-');

            int titleID = int.Parse(gonderilenVeri[0]); //titleıd döner
            int pageID = int.Parse(gonderilenVeri[1]);  // entryid döner

            //var title = point.TitleService.FindByID(titleID); // başlığın adı dönüyor buradan

            ICollection<Entry> el = point.EntryService.GetEntryList(titleID); //başlığa ait entryler

            var list = el.OrderBy(x => x.ID).ToPagedList(pageID, 5);

            return View(list);
        }

        [Login]
        public ActionResult Sozlesme()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kaydet()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string icerik = Request.Form["txtEntry"];
            string baslik = Request.Form["txtBaslikId"];
            string kullanici = Request.Form["txtUserId"];

            var baslikId = point.TitleService.GetID(baslik);
            var kullaniciId = point.UserService.GetID(kullanici);


            if (icerik == "")
            {
                return RedirectToAction("Basliklar/" + baslikId);
            }
            else
            {
                Entry ent = new Entry
                {
                    Content = icerik,
                    EntryDate = DateTime.Now,
                    UpCount = 0,
                    DownCount = 0,
                    UserID = kullaniciId,
                    TitleID = baslikId,
                    EntryUpdateDate=DateTime.Now,
                    isUpdated=false,
                    IPAddress=HelperClass.GetIpHelper()
                };
                Title tit = new Title
                {
                    ID = baslikId,
                    Content = baslik,
                    UpdateDate = DateTime.Now
                };
                MessageService msc = point.EntryService.Insert(ent);
                MessageService mscUp = point.TitleService.Update(tit);
            }
            return RedirectToAction("Basliklar/" + baslikId);
        }

        [Login]
        public ActionResult Kullanici(int? id)
        {
            if (id == null || id == 0) 
            {
                id = 1;
            }

            var userList = point.UserService.FindByID(id.Value);

            ICollection<Entry> userEnt = point.EntryService.ToList();

            List<Entry> ekle = new List<Entry>();

            foreach (var item in userEnt)
            {
                if (userList.ID == item.User.ID)
                {
                    ekle.Add(item);
                }
            }

            return View(ekle);
        }

        [Login]
        public ActionResult Entry(int? id)
        {
            if (id == null || id == 0)
            {
                id = 1;
            }

            List<Entry> entList = new List<Entry>();

            var ent = point.EntryService.FindByID(id.Value);

            entList.Add(ent);
            return View(entList);
        }

        [Login]
        [HttpPost]
        public ActionResult Sil(string id)
        {
            //var isim = HttpContext.User.Identity.Name;
            //int idendi = point.UserService.GetID(isim);

            string[] gonderilenVeri = id.Split('-');

            int entry = int.Parse(gonderilenVeri[0]);
            int title = int.Parse(gonderilenVeri[1]);

            var deleted = point.EntryService.Delete(entry);

            var list = point.EntryService.GetEntryList(title);


            return PartialView("KullaniciEntryListelePartial",list);
        }

        [Login]
        [HttpPost]
        public ActionResult Duzenle(Entry ent)
        {
            Entry updEnt = new Entry
            {
                ID=ent.ID,
                Content=ent.Content,
                EntryUpdateDate=DateTime.Now,
                isUpdated=true
            };
            MessageService ms = point.EntryService.Update(updEnt);
            int titleID = point.TitleService.GetID(updEnt.ID);

            //return RedirectToAction("Index");
            return RedirectToAction("Basliklar/" + titleID);
        }

        [HttpPost]
        public ActionResult Listele(int? id)
        {
            //entry id gelecek
            if (id == null || id == 0) 
            {
                return PartialView("KullaniciEntryListelePartial", point.EntryService.ToList());
            }
            var ent = point.EntryService.GetEntry(id.Value);
            
            return PartialView("KullaniciEntryListelePartial", ent);
        }
    }
}