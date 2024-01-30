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
    public class NganhngheCt2Controller : Controller
    {
        private readonly VNETCContext _context;

        public NganhngheCt2Controller(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/NganhngheCt2
        public async Task<IActionResult> Index()
        {
            return View(await _context.TNganhngheCt2s.ToListAsync());
        }

        // GET: Admin/NganhngheCt2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt2 = await _context.TNganhngheCt2s
                .FirstOrDefaultAsync(m => m.NganhngheCt2id == id);
            if (tNganhngheCt2 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt2);
        }

        // GET: Admin/NganhngheCt2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NganhngheCt2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NganhngheCt2id,MaNn,TenNn")] TNganhngheCt2 tNganhngheCt2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNganhngheCt2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNganhngheCt2);
        }

        // GET: Admin/NganhngheCt2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt2 = await _context.TNganhngheCt2s.FindAsync(id);
            if (tNganhngheCt2 == null)
            {
                return NotFound();
            }
            return View(tNganhngheCt2);
        }

        // POST: Admin/NganhngheCt2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NganhngheCt2id,MaNn,TenNn")] TNganhngheCt2 tNganhngheCt2)
        {
            if (id != tNganhngheCt2.NganhngheCt2id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNganhngheCt2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNganhngheCt2Exists(tNganhngheCt2.NganhngheCt2id))
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
            return View(tNganhngheCt2);
        }

        // GET: Admin/NganhngheCt2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt2 = await _context.TNganhngheCt2s
                .FirstOrDefaultAsync(m => m.NganhngheCt2id == id);
            if (tNganhngheCt2 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt2);
        }

        // POST: Admin/NganhngheCt2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNganhngheCt2 = await _context.TNganhngheCt2s.FindAsync(id);
            _context.TNganhngheCt2s.Remove(tNganhngheCt2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNganhngheCt2Exists(int id)
        {
            return _context.TNganhngheCt2s.Any(e => e.NganhngheCt2id == id);
        }
    }
}
