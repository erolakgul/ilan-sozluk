using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;

namespace Sozilan.UI.Models.Service
{
    public class EntryOfTitleOfUser
    {
        ServicePoint point;
        ICollection<Title> titList;
        ICollection<Entry> entList;
        ICollection<User> userList;

        public EntryOfTitleOfUser()
        {
            point = new ServicePoint();
            titList = point.TitleService.ToList();
            entList = point.EntryService.ToList();
            userList = point.UserService.ToList();
        }

        public int EntryID { get; set; }
        public int TitleID { get; set; }
        public int UserID { get; set; }

        public string EntryContent { get; set; }
        public string TitleContent { get; set; }
        public string NameOfUser { get; set; }

        public int Count { get; set; }
        public System.DateTime TimeOfComment { get; set; }

    }
}