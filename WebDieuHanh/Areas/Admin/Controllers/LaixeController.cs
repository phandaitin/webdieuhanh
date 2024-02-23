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
    public class LaixeController : Controller
    {
        private readonly webdieuhanhContext _context;

        public LaixeController(webdieuhanhContext context)
        {
            _context = context;
        }

        // GET: Admin/Laixe
        public async Task<IActionResult> Index()
        {
            return View(await _context.TLaixes.ToListAsync());
        }

        // GET: Admin/Laixe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLaixe = await _context.TLaixes
                .FirstOrDefaultAsync(m => m.LaixeId == id);
            if (tLaixe == null)
            {
                return NotFound();
            }

            return View(tLaixe);
        }

        // GET: Admin/Laixe/Create
        public IActionResult Create()
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");

            return View();
        }

        // POST: Admin/Laixe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaixeId,Hoten,Email,Phone1,Phone2,Phone3,Dob,MaDv")] TLaixe tLaixe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLaixe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tLaixe);
        }

        // GET: Admin/Laixe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");

            if (id == null)
            {
                return NotFound();
            }

            var tLaixe = await _context.TLaixes.FindAsync(id);
            if (tLaixe == null)
            {
                return NotFound();
            }
            return View(tLaixe);
        }

        // POST: Admin/Laixe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LaixeId,Hoten,Email,Phone1,Phone2,Phone3,Dob,MaDv")] TLaixe tLaixe)
        {
            if (id != tLaixe.LaixeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLaixe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLaixeExists(tLaixe.LaixeId))
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
            return View(tLaixe);
        }

        // GET: Admin/Laixe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLaixe = await _context.TLaixes
                .FirstOrDefaultAsync(m => m.LaixeId == id);
            if (tLaixe == null)
            {
                return NotFound();
            }

            return View(tLaixe);
        }

        // POST: Admin/Laixe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tLaixe = await _context.TLaixes.FindAsync(id);
            _context.TLaixes.Remove(tLaixe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TLaixeExists(int id)
        {
            return _context.TLaixes.Any(e => e.LaixeId == id);
        }
    }
}
