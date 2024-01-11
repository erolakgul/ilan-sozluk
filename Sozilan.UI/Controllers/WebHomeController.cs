using System;
using System.Collections;   // en son eklendi
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sozilan.Core.BaseService;
using Sozilan.Data.DataContext;
using Sozilan.Data.Model;
using Sozilan.UI.Models.Service;
using PagedList;
using PagedList.Mvc;

namespace Sozilan.UI.Controllers
{
    public class WebHomeController : Controller
    {
        // GET: WebHome
        ServicePoint point;
        TitleView viewModel = null;

        public WebHomeController()
        {
            point = new ServicePoint();
        }

        public ActionResult Index()
        {
            //ICollection<Title> titleList = point.TitleService.ToList();
            //return View(titleList);
            int sonId = point.TitleService.LastID(); //başlıkların son id sinin nosu nu çekiyorz

            Random r = new Random();
            List<int> sayiDizi = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                int rSayi = r.Next(1, sonId + 1);
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
                // 10 dan az ise entry sayısı olduğu kadar ı gösterilecek
                var little = el.OrderBy(x => x.ID).ToPagedList(1, el.Count);
                return View(little);
            }

            var list = el.OrderBy(x => x.ID).ToPagedList(1, 5);
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

        [HttpPost]
        public ActionResult Arttir(int? id)
        {
            if (id == null) 
            { 
                return RedirectToAction("Index");
            }
            else
            {
                Entry ent = point.EntryService.FindByID(id.Value);

                ent.UpCount = ent.UpCount + 1;

                point.EntryService.UpdateRating(ent);
                

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Azalt(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Entry ent = point.EntryService.FindByID(id.Value);

                ent.DownCount = ent.DownCount - 1;

                point.EntryService.UpdateRating(ent);


                return RedirectToAction("Index");
            }
        }

        public ActionResult Kullanici(int? id)
        {
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

        public ActionResult Entry(int? id)
        {
            List<Entry> entList = new List<Entry>();

            var ent = point.EntryService.FindByID(id.Value);

            entList.Add(ent);
            return View(entList);
        }
        //[HttpPost]
        //public ActionResult Duzenle(Entry ent)
        //{
        //    Entry updEnt = new Entry
        //    {
        //        ID = ent.ID,
        //        Content = ent.Content,
        //        EntryUpdateDate = DateTime.Now,
        //        isUpdated = true
        //    };
        //    MessageService ms = point.EntryService.Update(updEnt);
        //    int titleID = point.TitleService.GetID(updEnt.ID);

        //    //var isim = HttpContext.User.Identity.Name;
        //    //int idendi = point.UserService.GetID(isim);
        //    //return RedirectToAction("Kullanici/" + idendi);
        //    return RedirectToAction("Basliklar/" + titleID);
        //}

        [HttpPost]
        public ActionResult Listele(int? id)
        {
            //entry id gelecek
            if (id == null || id == 0)
            {
                return PartialView("KullaniciEntryListelePartial", point.EntryService.ToList());
            }
            var ent = point.EntryService.GetEntry(id.Value);

            //return RedirectToAction("Kullanici/");
            return PartialView("KullaniciEntryListelePartial", ent);
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult İletisim()
        {
            return View();
        }
        public ActionResult Sozlesme()
        {
            return View();
        }
        /// <summary>
        ///  arama işlemlerini gerçekleştireceğimiz kısım
        /// </summary>
        public async Task<PartialViewResult> Search(string searchKey)
        {
            var tasks = new Task[1];
            int i = 0;

            viewModel = new TitleView();

            viewModel.SearchKey = searchKey;

            List<Task> TaskList = GetSeachResult(searchKey, viewModel);
            foreach (Task tsk in TaskList)
            {
                tasks[i] = tsk;
                i++;
            }
            await Task.WhenAll(tasks);

            return PartialView("ResultView", viewModel);
        }

        private List<Task> GetSeachResult(string search, TitleView model)
        {
            // gelen search verisi #kariyer-mar  şeklinde olacak

            List<Task> Tasks = new List<Task>();
            var taskCustomer = Task.Factory.StartNew(() =>
            {
                model.TitleList = point.TitleService.GetTitle(search).ToList();
            });
            Tasks.Add(taskCustomer);
            return Tasks;
        }
    }
}