using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
using System.Web;
using System.Web.Mvc;
using Sozilan.Core.BaseService;
using Sozilan.Data.DataContext;
using Sozilan.Data.Model;
using Sozilan.UI.Models.Service;

namespace Sozilan.UI.Controllers
{
    public class TitleController : Controller
    {
        ServicePoint point;

        EntryOfTitleOfUser mix;
        //List<Entry> _entList;
        //List<Title> _titList;
        //List<User> _userList;
        ManagementSystemContext db = new ManagementSystemContext();
        List<EntryOfTitleOfUser> eofLis;

        public TitleController()
        {
            point = new ServicePoint();

            // şimdilik user id si 1 olan bağlı title ve entry lerin listelendiği sorgu
            // burada mix,title ile entry nin join olduğu view ün adı gibi düşünülebilir
            var koleksiyon = db.Title.Join(db.Enter, tit => tit.ID, ent => ent.TitleID, (tit, ent) => new { tit, ent })
                           .Join(db.User, mix => mix.ent.UserID, use => use.ID, (mix, use) => new { mix, use })
                           .Where(m => m.use.ID == 1).ToList();

            eofLis = new List<EntryOfTitleOfUser>();

            foreach (var item in koleksiyon)
            {
                mix = new EntryOfTitleOfUser
                {
                    EntryID = item.mix.ent.ID,
                    TitleID = item.mix.tit.ID,
                    UserID = item.use.ID,

                    EntryContent = item.mix.ent.Content,
                    TitleContent = item.mix.tit.Content,
                    NameOfUser = item.use.UserName,

                    TimeOfComment = item.mix.ent.EntryDate
                };
                eofLis.Add(mix);
            }

        }
        // GET: Title
        public ActionResult Index()
        {
            return View(eofLis);
        }
    }
}