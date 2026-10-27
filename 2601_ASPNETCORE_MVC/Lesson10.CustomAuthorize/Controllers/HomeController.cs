using Lesson10.CustomAuthorize.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lesson10.CustomAuthorize.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly XuongMocContext _context;

        public HomeController(ILogger<HomeController> logger, XuongMocContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var hash = GetSha256Hash(model.Password);

            var admin = _context.AdminUsers
                .FirstOrDefault(a => a.Email == model.Email && a.Password == hash);

            if (admin != null)
            {
                await SignInAsync(admin.Email, "Admin", admin.Name ?? "Admin");
                return RedirectToAction("Index", "Admin");
            }

            var customer = _context.Customers
                .FirstOrDefault(c => (c.Email == model.Email || c.Username == model.Email)
                                     && c.Password == hash
                                     && c.Isdelete != 1);

            if (customer != null)
            {
                await SignInAsync(customer.Email, "Customer", customer.Name ?? "Khách");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Tài khoản không hợp lệ");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng kiểm tra thông tin";
                return View("Login");
            }

            if (_context.Customers.Any(c => c.Email == model.Email))
            {
                ViewBag.Error = "Email đã tồn tại";
                return View("Login");
            }

            if (_context.Customers.Any(c => c.Username == model.Username))
            {
                ViewBag.Error = "Username đã tồn tại";
                return View("Login");
            }

            var customer = new Customer
            {
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Password = GetSha256Hash(model.Password),
                CreatedDate = DateTime.Now,
                Isactive = 1,
                Isdelete = 0
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            ViewBag.Success = "Đăng ký thành công! Vui lòng đăng nhập.";
            return View("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("DevSecuritySchema");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task SignInAsync(string email, string role, string fullName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim("FullName", fullName)
            };

            var identity = new ClaimsIdentity(claims, "DevSecuritySchema");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("DevSecuritySchema", principal);
        }

        private static string GetSha256Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}