using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class XeController : Controller
    {
        private readonly webdieuhanhContext _context;

        public XeController(webdieuhanhContext context)
        {
            _context = context;
        }

        // GET: Admin/Xe
        public async Task<IActionResult> Index()
        {
            return View(await _context.TXes.ToListAsync());
        }

        // GET: Admin/Xe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tXe = await _context.TXes
                .FirstOrDefaultAsync(m => m.XeId == id);
            if (tXe == null)
            {
                return NotFound();
            }

            return View(tXe);
        }

        // GET: Admin/Xe/Create
        public IActionResult Create()
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");
            ViewData["LaixeId"] = new SelectList(_context.TLaixes, "LaixeId", "Hoten");

            return View();
        }

        // POST: Admin/Xe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("XeId,Soxe,Loaixe,NgayDangKiem,HanBaoHiem,NgayDaiTuGanNhat,Tinhtrang,MaDv,LaixeId ,Ghichu")] TXe tXe)
        {

            tXe.Soxe = tXe.Soxe.ToUpper();

            if (ModelState.IsValid)
            {
                _context.Add(tXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tXe);
        }

        // GET: Admin/Xe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");
            ViewData["LaixeId"] = new SelectList(_context.TLaixes, "LaixeId", "Hoten");

            if (id == null)
            {
                return NotFound();
            }

            var tXe = await _context.TXes.FindAsync(id);
            if (tXe == null)
            {
                return NotFound();
            }
            return View(tXe);
        }

        // POST: Admin/Xe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("XeId,Soxe,Loaixe,NgayDangKiem,HanBaoHiem,NgayDaiTuGanNhat,Tinhtrang, MaDv, LaixeId, Ghichu")] TXe tXe)
        {
            if (id != tXe.XeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                tXe.Soxe = tXe.Soxe.ToUpper();
                try
                {
                    _context.Update(tXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TXeExists(tXe.XeId))
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
            return View(tXe);
        }

        // GET: Admin/Xe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tXe = await _context.TXes
                .FirstOrDefaultAsync(m => m.XeId == id);
            if (tXe == null)
            {
                return NotFound();
            }

            return View(tXe);
        }

        // POST: Admin/Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tXe = await _context.TXes.FindAsync(id);
            _context.TXes.Remove(tXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TXeExists(int id)
        {
            return _context.TXes.Any(e => e.XeId == id);
        }
    }
}
