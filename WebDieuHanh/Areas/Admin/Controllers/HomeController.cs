using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Model;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]

    [Area("Admin")]
    public class HomeController : Controller
    {
 

        private readonly webdieuhanhContext _context;

        public HomeController(webdieuhanhContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            //var menu = _context.TMenus.Where(x => x.Active == true).OrderBy(x => x.Tt).ToList();
            //var donvi = _context.TDonvis.OrderBy(x => x.MaDv).ToList();
            ////==================================
            //var items = new HomeVM()
            //{
            //    Menu = menu ,
            //    Donvi = donvi
            //};
            //return View(items);


            return View();
        }




    //================================================
    }
}
