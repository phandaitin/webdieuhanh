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
using VNETC_WebApp.Areas.Admin.Helper;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Authorize]

    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly VNETCContext _context;

        public PostController(VNETCContext context)
        {
            _context = context;
        }

        // GET: Admin/Post
        public async Task<IActionResult> Index()
        {
            return View(await _context.TPosts.OrderByDescending(x => x.PostId).Take(50).ToListAsync());
        }

        // GET: Admin/Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tPost = await _context.TPosts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tPost == null)
            {
                return NotFound();
            }

            return View(tPost);
        }

        // GET: Admin/Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Tencongtrinh , Chudautu ,Website,PostName,Content,Thumb1,Thumb2,Thumb3,Slug,TimeStart,TimeEnd,Author")] TPost tPost, IFormFile thumb1, IFormFile thumb2, IFormFile thumb3)
        {
            if (thumb1 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "post", thumb1.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await thumb1.CopyToAsync(file);
                };
                tPost.Thumb1 = thumb1.FileName; 
            }
            if (thumb2 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "post", thumb2.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await thumb2.CopyToAsync(file);
                };
                tPost.Thumb2 = thumb2.FileName;
            }
            if (thumb3 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "post", thumb3.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await thumb3.CopyToAsync(file);
                };
                tPost.Thumb3 = thumb3.FileName;
            }

            //===============================================
            tPost.Slug = Utils.SEOUrl(tPost.PostName);
            //===============================================
            if (ModelState.IsValid)
            {
                _context.Add(tPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tPost);
        }

        // GET: Admin/Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tPost = await _context.TPosts.FindAsync(id);
            if (tPost == null)
            {
                return NotFound();
            }
            return View(tPost);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId, Tencongtrinh , Chudautu ,Website, PostName,Content,Thumb1,Thumb2,Thumb3,Slug,TimeStart,TimeEnd,Author")] TPost tPost, IFormFile newThumb1, IFormFile newThumb2, IFormFile newThumb3)
        {
            if (newThumb1 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "post", newThumb1.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb1.CopyToAsync(file);
                };
                tPost.Thumb1 = newThumb1.FileName;
            }

            if (newThumb2 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "post", newThumb2.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb2.CopyToAsync(file);
                };
                tPost.Thumb2 = newThumb2.FileName;
            }

            if (newThumb3 != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "post", newThumb3.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    await newThumb3.CopyToAsync(file);
                };
                tPost.Thumb3 = newThumb3.FileName;
            }

            //=====================================


            if (id != tPost.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TPostExists(tPost.PostId))
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
            return View(tPost);
        }

        // GET: Admin/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tPost = await _context.TPosts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tPost == null)
            {
                return NotFound();
            }

            return View(tPost);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tPost = await _context.TPosts.FindAsync(id);
            _context.TPosts.Remove(tPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TPostExists(int id)
        {
            return _context.TPosts.Any(e => e.PostId == id);
        }
    }
}
