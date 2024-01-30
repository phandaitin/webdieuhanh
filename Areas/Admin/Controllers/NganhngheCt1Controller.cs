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
    public class NganhngheCt1Controller : Controller
    {
        private readonly VNETCContext _context;

        public NganhngheCt1Controller(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/NganhngheCt1
        public async Task<IActionResult> Index()
        {
            return View(await _context.TNganhngheCt1s.ToListAsync());
        }

        // GET: Admin/NganhngheCt1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt1 = await _context.TNganhngheCt1s
                .FirstOrDefaultAsync(m => m.NganhngheCt1id == id);
            if (tNganhngheCt1 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt1);
        }

        // GET: Admin/NganhngheCt1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NganhngheCt1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NganhngheCt1id,MaNn,TenNn")] TNganhngheCt1 tNganhngheCt1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNganhngheCt1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNganhngheCt1);
        }

        // GET: Admin/NganhngheCt1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt1 = await _context.TNganhngheCt1s.FindAsync(id);
            if (tNganhngheCt1 == null)
            {
                return NotFound();
            }
            return View(tNganhngheCt1);
        }

        // POST: Admin/NganhngheCt1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NganhngheCt1id,MaNn,TenNn")] TNganhngheCt1 tNganhngheCt1)
        {
            if (id != tNganhngheCt1.NganhngheCt1id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNganhngheCt1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNganhngheCt1Exists(tNganhngheCt1.NganhngheCt1id))
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
            return View(tNganhngheCt1);
        }

        // GET: Admin/NganhngheCt1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt1 = await _context.TNganhngheCt1s
                .FirstOrDefaultAsync(m => m.NganhngheCt1id == id);
            if (tNganhngheCt1 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt1);
        }

        // POST: Admin/NganhngheCt1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNganhngheCt1 = await _context.TNganhngheCt1s.FindAsync(id);
            _context.TNganhngheCt1s.Remove(tNganhngheCt1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNganhngheCt1Exists(int id)
        {
            return _context.TNganhngheCt1s.Any(e => e.NganhngheCt1id == id);
        }
    }
}
