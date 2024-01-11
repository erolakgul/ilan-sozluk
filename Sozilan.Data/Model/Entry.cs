using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Data.DataContext;

namespace Sozilan.Data.Model
{
    public class Entry : BaseClass
    {
        public Entry()
        {
            //this.Users = new List<User>();
        }
        [Required]
        public string Content { get; set; }
        public System.DateTime EntryDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public int UpCount { get; set; }
        public int DownCount { get; set; }
        public bool isUpdated { get; set; }

        public Nullable<int> TitleID { get; set; }
        // title ın id sini alır  --> YORUM HANGİ BAŞLIĞA AİT
        //[ForeignKey("TitleID")]
        public virtual Title Title { get; set; }

        public Nullable<int> UserID { get; set; }
        // users a id gönderir    --> YORUM KİM TARAFINDAN YAZILMIŞ ? BİLGİSİ ARA TABLODA TUTULACAK
        //[ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
