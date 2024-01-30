using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DichvuController : Controller
    {
        private readonly VNETCContext _context;

        public DichvuController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Dichvu
        public async Task<IActionResult> Index()
        {
            return View(await _context.TDichvus.ToListAsync());
        }

        // GET: Admin/Dichvu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDichvu = await _context.TDichvus
                .FirstOrDefaultAsync(m => m.DichvuId == id);
            if (tDichvu == null)
            {
                return NotFound();
            }

            return View(tDichvu);
        }

        // GET: Admin/Dichvu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dichvu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DichvuId,TenDv,Mota")] TDichvu tDichvu)
        {
            int itemCount = _context.TDichvus.Count();
            if (itemCount > 5) 
            {
                return Content( "Chỉ nhập 6 dịch vụ chính");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tDichvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tDichvu);
        }

        // GET: Admin/Dichvu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDichvu = await _context.TDichvus.FindAsync(id);
            if (tDichvu == null)
            {
                return NotFound();
            }
            return View(tDichvu);
        }

        // POST: Admin/Dichvu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DichvuId,TenDv,Mota")] TDichvu tDichvu)
        {
            if (id != tDichvu.DichvuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDichvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDichvuExists(tDichvu.DichvuId))
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
            return View(tDichvu);
        }

        // GET: Admin/Dichvu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDichvu = await _context.TDichvus
                .FirstOrDefaultAsync(m => m.DichvuId == id);
            if (tDichvu == null)
            {
                return NotFound();
            }

            return View(tDichvu);
        }

        // POST: Admin/Dichvu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDichvu = await _context.TDichvus.FindAsync(id);
            _context.TDichvus.Remove(tDichvu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDichvuExists(int id)
        {
            return _context.TDichvus.Any(e => e.DichvuId == id);
        }
    }
}
