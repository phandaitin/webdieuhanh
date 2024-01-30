using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NganhngheController : Controller
    {
        private readonly VNETCContext _context;

        public NganhngheController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Nganhnghe
        public async Task<IActionResult> Index()
        {
            return View(await _context.TNganhnghes.ToListAsync());
        }

        // GET: Admin/Nganhnghe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhnghe = await _context.TNganhnghes
                .FirstOrDefaultAsync(m => m.NganhngheId == id);
            if (tNganhnghe == null)
            {
                return NotFound();
            }

            return View(tNganhnghe);
        }

        // GET: Admin/Nganhnghe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nganhnghe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NganhngheId,Tennganhnghe1,TenCongty,Gioithieu,Tentienganh,Tenviettat,Mst,Nguoidaidien,DiaChi,Dienthoai,Email,Loaihinhdoanhnghiep,SoDkkd,Quanlyboi,Thumbnganhnghe1,Tennganhnghe2,Thumbnganhnghe2,Tennganhnghe3,Thumbnganhnghe3,Tennganhnghe4,Thumbnganhnghe4")] TNganhnghe tNganhnghe , IFormFile thumb1, IFormFile thumb2, IFormFile thumb3, IFormFile thumb4 )
        {
            if (thumb1 != null)
            {
                var pathThumb  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", thumb1.FileName);
                using (var file = new FileStream(pathThumb, FileMode.Create))
                {
                    await thumb1.CopyToAsync(file);
                };
                 tNganhnghe.Thumbnganhnghe1 = thumb1.FileName;
            }

            if (thumb2 != null)
            {
                var pathThumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", thumb2.FileName);
                using (var file = new FileStream(pathThumb, FileMode.Create))
                {
                    await thumb2.CopyToAsync(file);
                };
                tNganhnghe.Thumbnganhnghe2 = thumb2.FileName;
            }

            if (thumb3 != null)
            {
                var pathThumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", thumb3.FileName);
                using (var file = new FileStream(pathThumb, FileMode.Create))
                {
                    await thumb1.CopyToAsync(file);
                };
                tNganhnghe.Thumbnganhnghe3 = thumb3.FileName;
            }

            if (thumb4 != null)
            {
                var pathThumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", thumb4.FileName);
                using (var file = new FileStream(pathThumb, FileMode.Create))
                {
                    await thumb4.CopyToAsync(file);
                };
                tNganhnghe.Thumbnganhnghe4 = thumb4.FileName;
            }


            if (ModelState.IsValid)
            {
                _context.Add(tNganhnghe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNganhnghe);
        }

        // GET: Admin/Nganhnghe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhnghe = await _context.TNganhnghes.FindAsync(id);
            if (tNganhnghe == null)
            {
                return NotFound();
            }
            return View(tNganhnghe);
        }

        // POST: Admin/Nganhnghe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NganhngheId,Tennganhnghe1,TenCongty,Gioithieu,Tentienganh,Tenviettat,Mst,Nguoidaidien,DiaChi,Dienthoai,Email,Loaihinhdoanhnghiep,SoDkkd,Quanlyboi,Thumbnganhnghe1,Tennganhnghe2,Thumbnganhnghe2,Tennganhnghe3,Thumbnganhnghe3,Tennganhnghe4,Thumbnganhnghe4")] TNganhnghe tNganhnghe, IFormFile newThumb1, IFormFile newThumb2, IFormFile newThumb3, IFormFile newThumb4)
        {
            if (newThumb1 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", newThumb1.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb1.CopyToAsync(file);
                };
               tNganhnghe.Thumbnganhnghe1  = newThumb1.FileName;
            }

            if (newThumb2 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", newThumb2.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb2.CopyToAsync(file);
                };
                tNganhnghe.Thumbnganhnghe2 = newThumb2.FileName;
            }

            if (newThumb3 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", newThumb3.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb3.CopyToAsync(file);
                };
                tNganhnghe.Thumbnganhnghe3 = newThumb3.FileName;
            }

            if (newThumb4 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "nganhnghe", newThumb4.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb4.CopyToAsync(file);
                };
                tNganhnghe.Thumbnganhnghe4 = newThumb4.FileName;
            }


            if (id != tNganhnghe.NganhngheId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNganhnghe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNganhngheExists(tNganhnghe.NganhngheId))
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
            return View(tNganhnghe);
        }

        // GET: Admin/Nganhnghe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNganhnghe = await _context.TNganhnghes
                .FirstOrDefaultAsync(m => m.NganhngheId == id);
            if (tNganhnghe == null)
            {
                return NotFound();
            }

            return View(tNganhnghe);
        }

        // POST: Admin/Nganhnghe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNganhnghe = await _context.TNganhnghes.FindAsync(id);
            _context.TNganhnghes.Remove(tNganhnghe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNganhngheExists(int id)
        {
            return _context.TNganhnghes.Any(e => e.NganhngheId == id);
        }
    }
}
