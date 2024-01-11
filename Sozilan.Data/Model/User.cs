using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sozilan.Data.Model
{
    public class User : BaseClass
    {
        public User()
        {
            this.Entries = new List<Entry>();
            this.Titles = new List<Title>();
            this.Messages = new List<Message>();
        }

        [Required(ErrorMessage = "Sana bir takma ad lazım sanki...")]
        [StringLength(50,ErrorMessage="50 karakteri aşacak nasıl bir nick in var :/")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mail gereklidir ki login olasın..")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter correct email address,bak ingilizce konuştum :)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli,neyle gircen başka?")]
        [DataType(DataType.Password)]
        [StringLength(10,MinimumLength=6,ErrorMessage="6-10 arası şifre kabulümüzdür..")]
        public string Password { get; set; }

        [Required(ErrorMessage="Az çok demeyelim boş geçilmeyelim..")]
        [Compare("Password",ErrorMessage="Şifreler eşleşmiyor,ne yazmıştın ki..")]
        public string ConfirmPassword { get; set; }

        public DateTime CreateDate { get; set; }
        public Int16 Role { get; set; }   // smallint=Int16,belki ilerde statu veririz,SEN İK SIN SEN İŞVERENSİN SEN İŞ ARAYANSIN VS 

        public string UserGUID { get; set; }

        // başlık tablosuna id sini gönderecek
        // bir kullanıcının birden fazla başlığı yada entry si olabilir
        public virtual ICollection<Title> Titles { get; set; }  
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}
