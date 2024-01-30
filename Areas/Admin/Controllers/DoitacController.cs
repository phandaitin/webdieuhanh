using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoitacController : Controller
    {
        private readonly VNETCContext _context;

        public DoitacController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Doitac
        public async Task<IActionResult> Index()
        {
            return View(await _context.TDoitacs.ToListAsync());
        }

        // GET: Admin/Doitac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDoitac = await _context.TDoitacs
                .FirstOrDefaultAsync(m => m.DoitacId == id);
            if (tDoitac == null)
            {
                return NotFound();
            }

            return View(tDoitac);
        }

        // GET: Admin/Doitac/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Doitac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoitacId,Logo,Link")] TDoitac tDoitac, IFormFile logo)
        {
                if (logo != null)
                {                    
                    var pathLogo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "doitac", logo.FileName);
                    using (var file = new FileStream(pathLogo, FileMode.Create))
                    {
                        await logo.CopyToAsync(file);
                    };
                tDoitac.Logo = logo.FileName;
                }

                if (ModelState.IsValid)
                    {
                        _context.Add(tDoitac);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
            return View(tDoitac);
        }

        // GET: Admin/Doitac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDoitac = await _context.TDoitacs.FindAsync(id);
            if (tDoitac == null)
            {
                return NotFound();
            }
            return View(tDoitac);
        }

        // POST: Admin/Doitac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoitacId,Logo,Link")] TDoitac tDoitac, IFormFile newLogo)
        {
            if (newLogo != null)
            {
                var pathLogo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "doitac", newLogo.FileName);                
                using (var file = new FileStream(pathLogo, FileMode.Create))
                {
                    await newLogo.CopyToAsync(file);
                };
                tDoitac.Logo = newLogo.FileName; 
            }


            if (id != tDoitac.DoitacId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDoitac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDoitacExists(tDoitac.DoitacId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDoitac);
        }

        // GET: Admin/Doitac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDoitac = await _context.TDoitacs
                .FirstOrDefaultAsync(m => m.DoitacId == id);
            if (tDoitac == null)
            {
                return NotFound();
            }

            return View(tDoitac);
        }

        // POST: Admin/Doitac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDoitac = await _context.TDoitacs.FindAsync(id);
            _context.TDoitacs.Remove(tDoitac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDoitacExists(int id)
        {
            return _context.TDoitacs.Any(e => e.DoitacId == id);
        }
    }
}
