using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaixeController : Controller
    {
        private readonly webdieuhanhContext _context;

        public LoaixeController(webdieuhanhContext context)
        {
            _context = context;
        }

        // GET: Admin/Loaixe
        public async Task<IActionResult> Index()
        {
            return View(await _context.TLoaixes.ToListAsync());
        }

        // GET: Admin/Loaixe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLoaixe = await _context.TLoaixes
                .FirstOrDefaultAsync(m => m.LoaixeId == id);
            if (tLoaixe == null)
            {
                return NotFound();
            }

            return View(tLoaixe);
        }

        // GET: Admin/Loaixe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Loaixe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaixeId,Tenloaixe,Ghichu")] TLoaixe tLoaixe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLoaixe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tLoaixe);
        }

        // GET: Admin/Loaixe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLoaixe = await _context.TLoaixes.FindAsync(id);
            if (tLoaixe == null)
            {
                return NotFound();
            }
            return View(tLoaixe);
        }

        // POST: Admin/Loaixe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaixeId,Tenloaixe,Ghichu")] TLoaixe tLoaixe)
        {
            if (id != tLoaixe.LoaixeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLoaixe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLoaixeExists(tLoaixe.LoaixeId))
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
            return View(tLoaixe);
        }

        // GET: Admin/Loaixe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLoaixe = await _context.TLoaixes
                .FirstOrDefaultAsync(m => m.LoaixeId == id);
            if (tLoaixe == null)
            {
                return NotFound();
            }

            return View(tLoaixe);
        }

        // POST: Admin/Loaixe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tLoaixe = await _context.TLoaixes.FindAsync(id);
            _context.TLoaixes.Remove(tLoaixe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TLoaixeExists(int id)
        {
            return _context.TLoaixes.Any(e => e.LoaixeId == id);
        }
    }
}
