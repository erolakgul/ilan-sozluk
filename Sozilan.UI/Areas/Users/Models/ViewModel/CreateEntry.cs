using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sozilan.UI.Areas.Users.Models.ViewModel
{
    public class CreateEntry
    {  
        [Required(ErrorMessage = "Entry boş bırakılmamalı..")]
        public string _entryContent { get; set; }
        public string _titleContent { get; set; }
        public int _titleID { get; set; }
    }
}