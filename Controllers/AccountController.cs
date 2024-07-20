using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models.Entity;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly PasswordHasher<Account> passwordHasher;

        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            passwordHasher = new PasswordHasher<Account>();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

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
                account.password = passwordHasher.HashPassword(account, account.password);
                var register = new Account
                {
                    userName = account.userName,
                    password = account.password,
                    email = account.email,
                    firstName = account.firstName,
                    lastName = account.lastName,
                    gender = account.gender,
                };

                await dbContext.Accounts.AddAsync(register);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            return View(account);
        }

    }
}
