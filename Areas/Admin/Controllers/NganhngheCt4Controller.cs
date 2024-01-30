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
    public class NganhngheCt4Controller : Controller
    {
        private readonly VNETCContext _context;

        public NganhngheCt4Controller(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/NganhngheCt4
        public async Task<IActionResult> Index()
        {
            return View(await _context.TNganhngheCt4s.ToListAsync());
        }

        // GET: Admin/NganhngheCt4/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt4 = await _context.TNganhngheCt4s
                .FirstOrDefaultAsync(m => m.NganhngheCt4id == id);
            if (tNganhngheCt4 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt4);
        }

        // GET: Admin/NganhngheCt4/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NganhngheCt4/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NganhngheCt4id,MaNn,TenNn")] TNganhngheCt4 tNganhngheCt4)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNganhngheCt4);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNganhngheCt4);
        }

        // GET: Admin/NganhngheCt4/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt4 = await _context.TNganhngheCt4s.FindAsync(id);
            if (tNganhngheCt4 == null)
            {
                return NotFound();
            }
            return View(tNganhngheCt4);
        }

        // POST: Admin/NganhngheCt4/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NganhngheCt4id,MaNn,TenNn")] TNganhngheCt4 tNganhngheCt4)
        {
            if (id != tNganhngheCt4.NganhngheCt4id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNganhngheCt4);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNganhngheCt4Exists(tNganhngheCt4.NganhngheCt4id))
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
            return View(tNganhngheCt4);
        }

        // GET: Admin/NganhngheCt4/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhngheCt4 = await _context.TNganhngheCt4s
                .FirstOrDefaultAsync(m => m.NganhngheCt4id == id);
            if (tNganhngheCt4 == null)
            {
                return NotFound();
            }

            return View(tNganhngheCt4);
        }

        // POST: Admin/NganhngheCt4/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNganhngheCt4 = await _context.TNganhngheCt4s.FindAsync(id);
            _context.TNganhngheCt4s.Remove(tNganhngheCt4);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNganhngheCt4Exists(int id)
        {
            return _context.TNganhngheCt4s.Any(e => e.NganhngheCt4id == id);
        }
    }
}
