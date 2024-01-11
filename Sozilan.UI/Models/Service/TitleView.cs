using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sozilan.Data.Model;

namespace Sozilan.UI.Models.Service   // arama işlemleri için oluşturuldu
{
    public class TitleView
    {
        public List<Title> TitleList { get; set; }
        public string SearchKey { get; set; }
    }

    public static class HtmlHelperExtensions
    {
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}