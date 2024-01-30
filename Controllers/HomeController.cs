using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VNETC_WebApp.Areas.Admin.Data;
using VNETC_WebApp.Areas.Admin.Models;
using VNETC_WebApp.Models;

namespace VNETC_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly VNETCContext _context;

        public HomeController(VNETCContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var team = _context.TTeams.ToList();
            var cauhinh = _context.TCauhinhs.ToList();
            var khachhang = _context.TKhachhangs.ToList();
            var doitac = _context.TDoitacs.ToList();
            var nganhnghe = _context.TNganhnghes.ToList();
            var nganhngheCt1 = _context.TNganhngheCt1s.OrderBy(x=>x.MaNn).ToList();
            var nganhngheCt2 = _context.TNganhngheCt2s.OrderBy(x=>x.MaNn).ToList();
            var nganhngheCt3 = _context.TNganhngheCt3s.OrderBy(x => x.MaNn).ToList();
            var nganhngheCt4 = _context.TNganhngheCt4s.OrderBy(x => x.MaNn).ToList();

            var dichvu = _context.TDichvus.ToList();
            var post   = _context.TPosts.OrderByDescending(x => x.PostId).Take(50).ToList();

            var items = new HomeVM()
            {
                Cauhinh =  cauhinh,
                Team    = team,
                Khachhang = khachhang,
                Doitac = doitac,

                Nganhnghe = nganhnghe,
                NganhngheCt1 = nganhngheCt1,
                NganhngheCt2 = nganhngheCt2,
                NganhngheCt3 = nganhngheCt3,
                NganhngheCt4 = nganhngheCt4, 

                Dichvu  = dichvu,
                Post    = post

            };
            return View(items);

        }




        //==============================================
    }
}
