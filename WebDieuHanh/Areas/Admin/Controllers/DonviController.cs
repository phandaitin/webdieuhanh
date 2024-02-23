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
    public class DonviController : Controller
    {
        private readonly webdieuhanhContext _context;

        public DonviController(webdieuhanhContext context)
        {
            _context = context;
        }

        // GET: Admin/Donvi
        public async Task<IActionResult> Index()
        {
            if (TempData["MsgErr"] != null)
                ViewBag.MsgErr = TempData["MsgErr"];

            return View(await _context.TDonvis.ToListAsync());
        }

        // GET: Admin/Donvi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDonvi = await _context.TDonvis
                .FirstOrDefaultAsync(m => m.MaDvId == id);
            if (tDonvi == null)
            {
                return NotFound();
            }

            return View(tDonvi);
        }

        // GET: Admin/Donvi/Create
        public IActionResult Create()
        {
            if (User.Identity.Name.ToLower() != "admin")
            {
                TempData["MsgErr"] = "Only Admin User to Create/Edit/Delete....";                 
                return RedirectToAction("Index");
            }
            return View(); 
        }

        // POST: Admin/Donvi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDvId,MaDv,TenDv")] TDonvi tDonvi)
        {
            tDonvi.MaDv = tDonvi.MaDv.ToUpper();

            if (ModelState.IsValid)
            {
                _context.Add(tDonvi);
                await _context.SaveChangesAsync();
                
                TempData["Msg"] = "Successfull...";

                return RedirectToAction(nameof(Index));
            }
            return View(tDonvi);
        }

        // GET: Admin/Donvi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDonvi = await _context.TDonvis.FindAsync(id);
            if (tDonvi == null)
            {
                return NotFound();
            }
            return View(tDonvi);
        }

        // POST: Admin/Donvi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDvId,MaDv,TenDv")] TDonvi tDonvi)
        {
            if (id != tDonvi.MaDvId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDonvi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDonviExists(tDonvi.MaDvId))
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
            return View(tDonvi);
        }

        // GET: Admin/Donvi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDonvi = await _context.TDonvis
                .FirstOrDefaultAsync(m => m.MaDvId == id);
            if (tDonvi == null)
            {
                return NotFound();
            }

            return View(tDonvi);
        }

        // POST: Admin/Donvi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDonvi = await _context.TDonvis.FindAsync(id);
            _context.TDonvis.Remove(tDonvi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDonviExists(int id)
        {
            return _context.TDonvis.Any(e => e.MaDvId == id);
        }
    }
}
