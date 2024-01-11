using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozilan.Data.Model
{
    public class Notification : BaseClass
    {
        // ayda database in bu tablosunu havalandırırız

        public string UserName { get; set; }
        public int BildirilenID { get; set; }
        
        public int EventID { get; set; }  // 0 = beğenme, 1 = başlık açma (7 eylül 2016)
        public int EntryID { get; set; }
        public int TitleID { get; set; }

        public DateTime EventDate { get; set; }
    }
}
