using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozilan.Data.Model
{
    public class Message : BaseClass
    {
        // mesajlar tablosu ayda bir havalandırılır,2 gün önce den kullanıcılara isterlerse ss almaları söylenir

        [Required(ErrorMessage = "Boş Mesaj Gönderilemez..")]
        [StringLength(140, ErrorMessage = "Mesaj uzunluğu en fazla 140 karakterdir..")]
        public string Content { get; set; }

        public string MesajGUID { get; set; }
        public DateTime SendDate { get; set; }
        public int ReceivingID { get; set; }
        public string Receiving { get; set; }
        
        // user dan id alınacak
        public Nullable<int> UserID { get; set; }
        public virtual User User { get; set; }

    }
}
