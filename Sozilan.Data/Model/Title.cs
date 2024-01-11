using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozilan.Data.Model
{
    public class Title : BaseClass
    {
        public Title()
        {
            this.Entries = new List<Entry>();
        }
        // daha sonra bir sınırlama getirebiliriz
        public string Content { get; set; }
        public System.DateTime ContentDate { get; set; }
        public System.DateTime UpdateDate { get; set; }

        public string TitleTag { get; set; }
        public string TitleLink { get; set; }  // ülker link paylaşanlar olur belki :)

        // user dan id alınacak
        public Nullable<int> UserID { get; set; }
        public virtual User User { get; set; }
        // başlıktan yorum tablosuna id gönderilecek
        public virtual ICollection<Entry> Entries { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
