using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Public;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DieudongController : Controller
    {
        private readonly webdieuhanhContext _context;

        public DieudongController(webdieuhanhContext context)
        {
            _context = context;
        }

        
        // GET: Admin/Dieudong
        public async Task<IActionResult> Index()
        {
            if (TempData["MsgErr"] != null)
                ViewBag.MsgErr = TempData["MsgErr"];

            //var items = _context.TUsers.ToListAsync();
            if (User.Identity.Name.ToLower() == "admin" || User.Identity.Name.ToLower() == "vanphong")
            {
                return View(await _context.TDieudongs.ToListAsync()  );
            }
            else
            {
                return View(_context.TDieudongs.Where(x => x.MaDv == ClsPublic._MaDv).OrderByDescending( x=>x.NgayTh ) );
            }
             
        }

        // GET: Admin/Dieudong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDieudong = await _context.TDieudongs
                .FirstOrDefaultAsync(m => m.DieudongId == id);
            if (tDieudong == null)
            {
                return NotFound();
            }

            return View(tDieudong);
        }

        // GET: Admin/Dieudong/Create
        public IActionResult Create()
        {
            ViewData["Tenloaixe"] = new SelectList(_context.TLoaixes, "Tenloaixe", "Tenloaixe");
            ViewData["Tenloaicongtrinh"] = new SelectList(_context.TLoaicongtrinhs, "Tenloaicongtrinh", "Tenloaicongtrinh");

            return View();
        }

        // POST: Admin/Dieudong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DieudongId,MaDv, TenDv,NgayDk,NgayTh,Tenloaixe,Tenloaicongtrinh,Noidung,NgayDuyet,Soxe,Hoten,Trangthaiduyet,Ghichu")] TDieudong tDieudong)
        {
            //===================================================
            if (tDieudong.Tenloaixe == "-1" || tDieudong.Tenloaicongtrinh == "-1")
            {
                ViewBag.MsgLogin = "Vui lòng nhập đủ thông tin...";
                return View();
            }
            //===================================================

            if (ModelState.IsValid)
            {
                _context.Add(tDieudong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tDieudong);
        }

        // GET: Admin/Dieudong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Tenloaixe"] = new SelectList(_context.TLoaixes, "Tenloaixe", "Tenloaixe");
            ViewData["Tenloaicongtrinh"] = new SelectList(_context.TLoaicongtrinhs, "Tenloaicongtrinh", "Tenloaicongtrinh");

            if (id == null)
            {
                return NotFound();
            }

            var tDieudong = await _context.TDieudongs.FindAsync(id);
            if (tDieudong == null)
            {
                return NotFound();
            }
            //=====================================================
            if (tDieudong.Trangthaiduyet != "Chờ duyệt")
            {
                TempData["MsgErr"] = "Chỉ được Sửa/Xóa khi trạng thái 'Chờ duyệt'...";
                return RedirectToAction("Index");
            }

            if (tDieudong.MaDv.ToUpper() != ClsPublic._MaDv.ToUpper())
            {
                TempData["MsgErr"] = "Chỉ được Sửa/Xóa đơn vị của mình...";
                return RedirectToAction("Index");
            }
            //=====================================================
            return View(tDieudong);
        }

        // POST: Admin/Dieudong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DieudongId,MaDv,TenDv,NgayDk,NgayTh,Tenloaixe,Tenloaicongtrinh,Noidung,NgayDuyet,Soxe,Hoten,Trangthaiduyet,Ghichu")] TDieudong tDieudong)
        {
            if (id != tDieudong.DieudongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDieudong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDieudongExists(tDieudong.DieudongId))
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
            return View(tDieudong);
        }

        public async Task<IActionResult> Duyet(int? id)
        {
            ViewData["Soxe"] = new SelectList(_context.TXes, "Soxe", "Soxe");
            ViewData["Hoten"] = new SelectList(_context.TLaixes, "Hoten", "Hoten");

            if (id == null)
            {
                return NotFound();
            }

            var tDieudong = await _context.TDieudongs.FindAsync(id);
            if (tDieudong == null)
            {
                return NotFound();
            }
            //=====================================================
            if (tDieudong.Trangthaiduyet != "Chờ duyệt")
            {
                TempData["MsgErr"] = "Chỉ được Sửa/Xóa khi trạng thái 'Chờ duyệt'...";
                return RedirectToAction("Index");
            }

            if (User.Identity.Name != "vanphong")
            {
                TempData["MsgErr"] = "Chỉ Văn phòng mới duyệt điều động...";
                return RedirectToAction("Index");
            }
            //=====================================================
            return View(tDieudong);
        }

        // POST: Admin/Dieudong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duyet(int id, [Bind("DieudongId,MaDv,TenDv,NgayDk,NgayTh,Tenloaixe,Tenloaicongtrinh,Noidung,NgayDuyet,Soxe,Hoten,Trangthaiduyet,Ghichu")] TDieudong tDieudong)
        {

            if (tDieudong.Soxe == "-1" || tDieudong.Hoten == "-1")
            {
                ViewBag.MsgErr = "Vui lòng nhập đủ thông tin...";
                return View();
            }

            if (tDieudong.Trangthaiduyet == "Không duyệt" && tDieudong.Ghichu is null)
            {
                ViewBag.MsgErr = "Cập nhật lý do không duyệt đăng ký...";
                return View();
            }

            //===================================================



            if (id != tDieudong.DieudongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDieudong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDieudongExists(tDieudong.DieudongId))
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
            return View(tDieudong);
        }

        // GET: Admin/Dieudong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDieudong = await _context.TDieudongs.FirstOrDefaultAsync(m => m.DieudongId == id);
            if (tDieudong == null)
            {
                return NotFound();
            }

            //=====================================================
            if (tDieudong.Trangthaiduyet != "Chờ duyệt")
            {
                TempData["MsgErr"] = "Chỉ được Sửa/Xóa khi trạng thái 'Chờ duyệt'..."; 
                return RedirectToAction("Index");
            }

            if (tDieudong.MaDv.ToUpper() != ClsPublic._MaDv.ToUpper())
            {
                TempData["MsgErr"] = "Chỉ được Sửa/Xóa đơn vị của mình...";
                return RedirectToAction("Index");
            }

            //=====================================================

            return View(tDieudong);
        }

        // POST: Admin/Dieudong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var tDieudong = await _context.TDieudongs.FindAsync(id);
           

            _context.TDieudongs.Remove(tDieudong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDieudongExists(int id)
        {
            return _context.TDieudongs.Any(e => e.DieudongId == id);
        }
    }
}
