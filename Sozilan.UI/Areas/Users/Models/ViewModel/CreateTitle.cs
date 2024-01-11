using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sozilan.UI.Areas.Users.Models.ViewModel
{
    public class CreateTitle
    {
        [Required(ErrorMessage = "Başlık girilmelidir..")]
        [StringLength(50,ErrorMessage="Başlık en fazla 50 karakter uzunluğunda olmalıdır..")]
        public string _content { get; set; }

        public string _titleTag { get; set; }

        [Required(ErrorMessage = "Başlığa ait ilk girdi olmalıdır..")]
        public string _entryContent { get; set; }

        [Required(ErrorMessage = "İlana ait yayın linki bu kısıma kopyalanmalıdır..")]
        public string _titleLink { get; set; }
    }
}