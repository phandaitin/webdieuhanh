using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Helpers;
using WebApp.Areas.Admin.Public;

namespace WebApp.Areas.Admin.Controllers
{

    [Authorize]

    [Area("Admin")]
    public class UserController : Controller
    {
        
        private readonly webdieuhanhContext _context;
        private readonly IConfiguration _configuration;

        public UserController(webdieuhanhContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #region tin code 
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        [AllowAnonymous]
        [HttpGet]// [Route("Login", Name = "Login")]        
        public async Task<IActionResult> Login()
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(TUser tUser)
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");
            //ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "MaDv");
            //===================================================
            if (tUser.MaDv  == "-1")
            {
                ViewBag.MsgLogin = "Vui lòng chọn đơn vị...";
                return View();
            }
            //===================================================
            if (string.IsNullOrEmpty(tUser.UserName) || string.IsNullOrEmpty(tUser.Password))
            {
                ViewBag.MsgLogin = "Vui lòng nhập đủ thông tin...";
                return View();
            }
            //===================================================
            var user = Authenticate(tUser);
            if (user != null)
            {
                ClsPublic._MaDv = user.MaDv.ToString().ToUpper();   // lay ma dvi dang nhap
                ClsPublic._UserId = user.UserId;          // lay UserID  dang nhap


                var token = GenerateToken(user);
                if (token != null)
                {
                    var userPrincipal = ValidateToken(token);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                        IsPersistent = false
                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties
                    );

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.MsgLogin = "Sai thông tin đăng nhập...";
                return View();
            }

        }
        private TUser Authenticate( TUser tUser)
        {
            //var user = _context.TUsers.SingleOrDefault(x => x.UserName.ToLower() == tUser.UserName.ToLower() && x.Password ==  tUser.Password );
            //var user = _context.TUsers.SingleOrDefault(x => x.UserName.ToLower() == tUser.UserName.ToLower() && x.Password == Utils.MD5(tUser.Password));
            var user = _context.TUsers.Where(x => x.MaDv.ToLower() == tUser.MaDv.ToLower() &&  x.UserName.ToLower() == tUser.UserName.ToLower()  && x.Password == Utils.MD5(tUser.Password)  ).FirstOrDefault();
            if (user != null)
            {
                ClsPublic._UserId = user.UserId;
                return user;
            }
            else
                return null;
        }
        private string GenerateToken(TUser user)
        {
            //Tokens được cấu hình bên file appsettings.json.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),                
                //new Claim(ClaimTypes.Role , string.Join(":",  roles ))// new Claim(ClaimTypes.Role,user.Role)
            };
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:SecretKey"]));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: _signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public ClaimsPrincipal ValidateToken(string tokenString)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,

                ValidIssuer = _configuration["Tokens:Issuer"],
                ValidAudience = _configuration["Tokens:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:SecretKey"]))
            };
            ClaimsPrincipal claimPrincipal = new JwtSecurityTokenHandler().ValidateToken(tokenString, validationParameters, out SecurityToken token);
            return claimPrincipal;
        }

        #endregion

        // GET: Admin/Users/ChangePass/5
        public IActionResult ChangePass(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUser = _context.TUsers.Find(id);
            if (tUser == null)
            {
                return NotFound();
            }
            return View(tUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePass(int id, TUser tUser)
        {
            if (id != tUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tUser.Password = Utils.MD5(tUser.Password);
                    _context.Update(tUser);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TUserExists(tUser.UserId))
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
            //return View(tUser);
            return RedirectToAction(nameof(Index));
        }
        //================= end tin code =======================================

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            if (TempData["MsgErr"] != null)
                ViewBag.MsgErr = TempData["MsgErr"]; //============= Giá trị ViewBag được trả ở đây để đẩy ra form ( Alert) nếu có lỗi.
            //================================================
            //var items = _context.TUsers.ToListAsync();
            if (User.Identity.Name.ToLower() == "admin")
            {
                return View(await _context.TUsers.ToListAsync());
            }
            else
            {
                return View( _context.TUsers.Where(x => x.MaDv == ClsPublic._MaDv) );
            }
             
            
        }

        // GET: Admin/User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUsers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tUser == null)
            {
                return NotFound();
            }

            return View(tUser);
        }

        // GET: Admin/User/Create
        public IActionResult Create()
        {
            ViewData["MaDv"] = new SelectList(_context.TDonvis, "MaDv", "TenDv");

            if (User.Identity.Name.ToLower() != "admin")
            {
                TempData["MsgErr"] = "Only Admin User to Create/Edit/Delete...."; // msg này hiện thị bên  view index nên cần đưa vào TempData để truyền qua từ view Create
                //ViewBag.MsgErr = TempData["MsgErr"];
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,MaDv,UserName,FullName,Password")] TUser tUser)
        {
            tUser.Password = Utils.MD5(tUser.Password);

            if (ModelState.IsValid)
            {                
                _context.Add(tUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tUser);
        }

        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.Name.ToLower() != "admin")
            {
                TempData["MsgErr"] = "Only Admin User to Create/Edit/Delete....";                 
                return RedirectToAction("Index");
            }
            //====================================================
            if (id == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUsers.FindAsync(id);
            if (tUser == null)
            {
                return NotFound();
            }
            return View(tUser);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,MaDv,UserName,FullName,Password")] TUser tUser)
        {
            if (id != tUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TUserExists(tUser.UserId))
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
            return View(tUser);
        }

        // GET: Admin/User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.Name.ToLower() != "admin")
            {
                TempData["MsgErr"] = "Only Admin User to Create/Edit/Delete....";
                return RedirectToAction("Index");
            }
            //====================================================
            if (id == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUsers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tUser == null)
            {
                return NotFound();
            }

            return View(tUser);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tUser = await _context.TUsers.FindAsync(id);
            _context.TUsers.Remove(tUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TUserExists(int id)
        {
            return _context.TUsers.Any(e => e.UserId == id);
        }
    }
}
