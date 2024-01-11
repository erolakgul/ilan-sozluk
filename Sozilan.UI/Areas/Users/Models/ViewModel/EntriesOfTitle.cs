using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozilan.UI.Areas.Users.Models.ViewModel
{
    public class EntriesOfTitle
    {
        public int _titleID { get; set; }
        public int _entryID { get; set; }
        public int _userID { get; set; }

        public string _titleContent { get; set; }
        public string _entryContent { get; set; }
        public string _entryUser { get; set; }

        public DateTime _entryUpdateDate { get; set; }
    }
}