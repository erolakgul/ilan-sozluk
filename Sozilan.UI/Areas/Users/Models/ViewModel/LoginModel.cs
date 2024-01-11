using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sozilan.UI.Areas.Users.Models.ViewModel
{
    public class LoginModel
    {
        //[DataType(DataType.EmailAddress)]
        // loginden gönderilecek olan kayıtların var olup olmadığı usermanagement tan kontrol edilcek
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(@"[A-Za-z0-9]{6,8}")]
        public string Password { get; set; }
    }
}