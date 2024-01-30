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
    public class NganhngheCt3Controller : Controller
    {
        private readonly VNETCContext _context;

        public NganhngheCt3Controller(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/NganhngheCt3
        public async Task<IActionResult> Index()
        {
            return View(await _context.TNganhngheCt3s.ToListAsync());
        }

        // GET: Admin/NganhngheCt3/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt3 = await _context.TNganhngheCt3s
                .FirstOrDefaultAsync(m => m.NganhngheCt3id == id);
            if (tNganhngheCt3 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt3);
        }

        // GET: Admin/NganhngheCt3/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NganhngheCt3/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NganhngheCt3id,MaNn,TenNn")] TNganhngheCt3 tNganhngheCt3)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNganhngheCt3);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNganhngheCt3);
        }

        // GET: Admin/NganhngheCt3/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt3 = await _context.TNganhngheCt3s.FindAsync(id);
            if (tNganhngheCt3 == null)
            {
                return NotFound();
            }
            return View(tNganhngheCt3);
        }

        // POST: Admin/NganhngheCt3/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NganhngheCt3id,MaNn,TenNn")] TNganhngheCt3 tNganhngheCt3)
        {
            if (id != tNganhngheCt3.NganhngheCt3id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNganhngheCt3);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNganhngheCt3Exists(tNganhngheCt3.NganhngheCt3id))
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
            return View(tNganhngheCt3);
        }

        // GET: Admin/NganhngheCt3/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt3 = await _context.TNganhngheCt3s
                .FirstOrDefaultAsync(m => m.NganhngheCt3id == id);
            if (tNganhngheCt3 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt3);
        }

        // POST: Admin/NganhngheCt3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNganhngheCt3 = await _context.TNganhngheCt3s.FindAsync(id);
            _context.TNganhngheCt3s.Remove(tNganhngheCt3);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNganhngheCt3Exists(int id)
        {
            return _context.TNganhngheCt3s.Any(e => e.NganhngheCt3id == id);
        }
    }
}
