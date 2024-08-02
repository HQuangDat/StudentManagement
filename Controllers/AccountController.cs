using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public AccountController(ApplicationDbContext dbContext, IPasswordHasher<Account> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        //Ham xu ly dang nhap
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.userName == username);
            if (account != null && VerifyPassword(account, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.userName),
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
        //Ham xu ly dang ky
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account account)
        {
            if (ModelState.IsValid)
            {
                account.password = _passwordHasher.HashPassword(account, account.password);
                account.Role = "User";
                await _dbContext.Accounts.AddAsync(account);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(account);
        }

        //Ham xu ly dang xuat
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(Account account, string password)
        {
            return _passwordHasher.VerifyHashedPassword(account, account.password, password) != PasswordVerificationResult.Failed;
        }

        //Ham xu ly Access Denied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}