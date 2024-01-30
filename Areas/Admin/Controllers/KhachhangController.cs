using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class KhachhangController : Controller
    {
        private readonly VNETCContext _context;

        public KhachhangController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Khachhang
        public async Task<IActionResult> Index()
        {
            return View(await _context.TKhachhangs.ToListAsync());
        }

        // GET: Admin/Khachhang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tKhachhang = await _context.TKhachhangs
                .FirstOrDefaultAsync(m => m.KhachhangId == id);
            if (tKhachhang == null)
            {
                return NotFound();
            }

            return View(tKhachhang);
        }

        // GET: Admin/Khachhang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Khachhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KhachhangId,Avarta,Hoten,Chucvu,Noidungdanhgia")] TKhachhang tKhachhang, IFormFile avarta)
        {
            if (avarta != null)
            {
                //var pathThumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "post", DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + avarta.FileName);
                var pathAvarta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "khachhang", avarta.FileName);
                using (var file = new FileStream(pathAvarta, FileMode.Create))
                {
                    await avarta.CopyToAsync(file);
                };
                tKhachhang.Avarta = avarta.FileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(tKhachhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tKhachhang);
        }

        // GET: Admin/Khachhang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tKhachhang = await _context.TKhachhangs.FindAsync(id);
            if (tKhachhang == null)
            {
                return NotFound();
            }
            return View(tKhachhang);
        }

        // POST: Admin/Khachhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KhachhangId,Avarta,Hoten,Chucvu,Noidungdanhgia")] TKhachhang tKhachhang)
        {
            if (id != tKhachhang.KhachhangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tKhachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TKhachhangExists(tKhachhang.KhachhangId))
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
            return View(tKhachhang);
        }

        // GET: Admin/Khachhang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tKhachhang = await _context.TKhachhangs
                .FirstOrDefaultAsync(m => m.KhachhangId == id);
            if (tKhachhang == null)
            {
                return NotFound();
            }

            return View(tKhachhang);
        }

        // POST: Admin/Khachhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tKhachhang = await _context.TKhachhangs.FindAsync(id);
            _context.TKhachhangs.Remove(tKhachhang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TKhachhangExists(int id)
        {
            return _context.TKhachhangs.Any(e => e.KhachhangId == id);
        }
    }
}
