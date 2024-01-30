using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VNETC_WebApp.Areas.Admin.Data;
using VNETC_WebApp.Areas.Admin.Helper;
using VNETC_WebApp.Areas.Admin.Models;
using VNETC_WebApp.Areas.Admin.Public;

namespace VNETC_WebApp.Areas.Admin.Controllers
{
    [Authorize]

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly VNETCContext _context;
        private readonly IConfiguration _configuration;         
        public UserController(VNETCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        //======================== tin code===============================
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginVM model)
        {
            
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.MsgLogin = "Vui lòng nhập đủ thông tin...";
                return View();
            }
            //===================================================
            var user = Authenticate(model);
            if (user != null)
            {
                
                
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
        private TUser Authenticate(LoginVM model)
        {
            var user = _context.TUsers.SingleOrDefault(x => x.UserName.ToLower() == model.UserName.ToLower() && x.Password == Utils.MD5(model.Password));
            //var user = _context.TUsers.Where(x => x.MaDv.ToLower() == model.MaDv.ToLower() &&  x.UserName.ToLower() == model.UserName.ToLower()  && x.Password ==  model.Password).FirstOrDefault();
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

        // GET: Admin/Users
        //public async Task<IActionResult> Index()
        public  IActionResult  Index()
        {
            return View( _context.TUsers.ToList());
            //return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details1(int? id)
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

        // GET: Admin/Users/Create
        public IActionResult Create1()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("UserId,UserName,Password")] TUser tUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tUser);
        }

     
        


        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit1(int? id)
        {
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

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(int id, [Bind("UserId,UserName,Password")] TUser tUser)
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

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete1(int? id)
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

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete1")]
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

