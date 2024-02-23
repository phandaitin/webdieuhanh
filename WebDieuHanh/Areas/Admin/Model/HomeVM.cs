using System.Collections.Generic;
using WebApp.Areas.Admin.Data;

namespace WebApp.Areas.Admin.Model
{
    public class HomeVM
    {
        public List<TMenu> Menu { get; set; }
        public List<TDonvi> Donvi { get; set; }
    }
}
