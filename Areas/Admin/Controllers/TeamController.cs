using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VNETC_WebApp.Areas.Admin.Data;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly VNETCContext _context;

        public TeamController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Team
        public async Task<IActionResult> Index()
        {
            return View(await _context.TTeams.ToListAsync());
        }
       
        // GET: Admin/Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTeam = await _context.TTeams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (tTeam == null)
            {
                return NotFound();
            }

            return View(tTeam);
        }

        // GET: Admin/Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Avarta,Hoten,Chucvu,Bangcap1,Bangcap2,Bangcap3,Facebook,Zalo,Instagram,Twitter")] TTeam tTeam, IFormFile avarta)
        {

            if (avarta != null)
            {
                //var pathThumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "post", DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + avarta.FileName);
                var pathAvarta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","assets", "img", "team" , avarta.FileName);
                using (var file = new FileStream(pathAvarta, FileMode.Create))
                {
                    await  avarta.CopyToAsync(file);
                };
                tTeam.Avarta  = avarta.FileName;
            }


            if (ModelState.IsValid)
            {
                _context.Add(tTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tTeam);
        }

        // GET: Admin/Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTeam = await _context.TTeams.FindAsync(id);
            if (tTeam == null)
            {
                return NotFound();
            }
            return View(tTeam);
        }

        // POST: Admin/Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Avarta,Hoten,Chucvu,Bangcap1,Bangcap2,Bangcap3,Facebook,Zalo,Instagram,Twitter")] TTeam tTeam, IFormFile newAvarta)
        {
            if (newAvarta != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "team", newAvarta.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newAvarta.CopyToAsync(file);
                };
                tTeam.Avarta  = newAvarta.FileName;
            }

            if (id != tTeam.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTeamExists(tTeam.TeamId))
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
            return View(tTeam);
        }

        // GET: Admin/Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTeam = await _context.TTeams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (tTeam == null)
            {
                return NotFound();
            }

            return View(tTeam);
        }

        // POST: Admin/Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tTeam = await _context.TTeams.FindAsync(id);
            _context.TTeams.Remove(tTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TTeamExists(int id)
        {
            return _context.TTeams.Any(e => e.TeamId == id);
        }
    }
}
