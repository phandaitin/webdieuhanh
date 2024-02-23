using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Areas.Admin.Data
{
    public partial class TMenu
    {
        public int Tt { get; set; }
        public string TenMenu { get; set; }
        public string Link { get; set; }
        public bool? Active { get; set; }
    }
}
