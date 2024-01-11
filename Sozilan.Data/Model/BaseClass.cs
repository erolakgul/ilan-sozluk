using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozilan.Data.Model
{
    public class BaseClass
    {
        [Key]
        public int ID { get; set; }

        public bool IsActive { get; set; }
        public string IPAddress { get; set; }  // başımızı ağrıtma ihtimalleri ve istatistik açısından lazım olabilr
    }
}
