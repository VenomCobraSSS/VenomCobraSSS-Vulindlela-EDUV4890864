using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vulindlela.Portal.Data;
using Vulindlela.Portal.Models;

namespace Vulindlela.Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly PortalDbContext _context;

        public AccountController(PortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User? user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username");

                return View(model);
            }

            byte[] attemptedHash = PasswordHasher.HashPassword(model.Password, user.Salt);

            if (!attemptedHash.SequenceEqual(user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid Password");

                return View(model);
            }

            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, model.Username)
            ];

            ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Appointment");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");

                return View(model);
            }

            byte[] salt = PasswordHasher.GenerateSalt();

            byte[] passwordHash = PasswordHasher.HashPassword(model.Password, salt);

            User user = new()
            {
                Username = model.Username,
                PasswordHash = passwordHash,
                Salt = salt
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
