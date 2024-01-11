using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozilan.UI.Areas.Users.Models.ViewModel
{
    public class RegisterModel
    {
        //user tablomuz için girilen verileri gönderecek olan sınıfımız
        public string _userName { get; set; }
        public string _eMail { get; set; }
        public string _password { get; set; }
        public DateTime _createDate { get; set; }
        public Int16 _role { get; set; }
        public string _code { get; set; }
        public string _telephone { get; set; }
        public bool  _isDeleted { get; set; }
    }
}
