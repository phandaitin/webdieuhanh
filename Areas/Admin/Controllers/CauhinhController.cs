using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CauhinhController : Controller
    {
        private readonly VNETCContext _context;

        public CauhinhController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Cauhinh
        public async Task<IActionResult> Index()
        {
            return View(await _context.TCauhinhs.ToListAsync());
        }

        // GET: Admin/Cauhinh/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCauhinh = await _context.TCauhinhs
                .FirstOrDefaultAsync(m => m.CauhinhId == id);
            if (tCauhinh == null)
            {
                return NotFound();
            }

            return View(tCauhinh);
        }

        // GET: Admin/Cauhinh/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cauhinh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CauhinhId,Welcome,Chungtoila,Khachhanghailong,Duandathicong,Namkinhnghiem,Giaithuong,Taisaolachungtoi,Thumbnail,Videogioithieucongty,Gioithieucongty,Camket1,Camket2,Camket3,Camket4,Camket5,Ketluan,Kehoach,KehoachThumb,KehoachNoidung,Tamnhin,TamnhinThumb,TamnhinNoidung,Sumenh,SumenhThumb,SumenhNoidung,Address,Email,Phone,Chinhanh1,Chinhanh2,Chinhanh3,Chinhanh4,Chinhanh5,Vpdd1,Vpdd2,Vpdd3,Vpdd4,Vpdd5")] TCauhinh tCauhinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCauhinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tCauhinh);
        }

        // GET: Admin/Cauhinh/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCauhinh = await _context.TCauhinhs.FindAsync(id);
            if (tCauhinh == null)
            {
                return NotFound();
            }
            return View(tCauhinh);
        }

        // POST: Admin/Cauhinh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CauhinhId,Welcome,Chungtoila,Khachhanghailong,Duandathicong,Namkinhnghiem,Giaithuong,Taisaolachungtoi,Thumbnail,Videogioithieucongty,Gioithieucongty,Camket1,Camket2,Camket3,Camket4,Camket5,Ketluan,Kehoach,KehoachThumb,KehoachNoidung,Tamnhin,TamnhinThumb,TamnhinNoidung,Sumenh,SumenhThumb,SumenhNoidung,Address,Email,Phone,Chinhanh1,Chinhanh2,Chinhanh3,Chinhanh4,Chinhanh5,Vpdd1,Vpdd2,Vpdd3,Vpdd4,Vpdd5")] TCauhinh tCauhinh)
        {
            if (id != tCauhinh.CauhinhId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCauhinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCauhinhExists(tCauhinh.CauhinhId))
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
            return View(tCauhinh);
        }

        // GET: Admin/Cauhinh/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCauhinh = await _context.TCauhinhs
                .FirstOrDefaultAsync(m => m.CauhinhId == id);
            if (tCauhinh == null)
            {
                return NotFound();
            }

            return View(tCauhinh);
        }

        // POST: Admin/Cauhinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCauhinh = await _context.TCauhinhs.FindAsync(id);
            _context.TCauhinhs.Remove(tCauhinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCauhinhExists(int id)
        {
            return _context.TCauhinhs.Any(e => e.CauhinhId == id);
        }
    }
}
