using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Controllers
{
    public class PostDetailController : Controller
    {
        private readonly VNETCContext _context;

        public PostDetailController(VNETCContext context)
        {
            _context = context;
        }

        [HttpGet("/post/{slug}")]
        public IActionResult Index(string slug)
        {
             var item  = _context.TPosts.Where(x => x.Slug == slug).ToList(); 
             return View(item);
        }
         
    }
}
