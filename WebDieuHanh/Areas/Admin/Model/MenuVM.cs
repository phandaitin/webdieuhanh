using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Model
{
    public class MenuVM
    {
        public int Tt { get; set; }
        public string TenMenu { get; set; }
        public string Link { get; set; }
        public bool? Active { get; set; }
    }
}
