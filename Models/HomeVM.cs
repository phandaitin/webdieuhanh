using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Models
{
    public class HomeVM
    {
        public List<TCauhinh> Cauhinh { get; set; }
        public List<TTeam> Team { get; set; }
        public List<TKhachhang> Khachhang  { get; set; }
        public List<TDoitac> Doitac  { get; set; }

        public List<TNganhnghe> Nganhnghe { get; set; }

        public List<TNganhngheCt1> NganhngheCt1 { get; set; }
        public List<TNganhngheCt2> NganhngheCt2 { get; set; }
        public List<TNganhngheCt3> NganhngheCt3 { get; set; }
        public List<TNganhngheCt4> NganhngheCt4 { get; set; }

        public List<TDichvu> Dichvu  { get; set; }
        public List<TPost> Post { get; set; }


    }
}
