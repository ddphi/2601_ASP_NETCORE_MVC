using Lesson09.Lab.Areas.Admins.Models;
using Lesson09.Lab.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lesson09.Lab.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class LoginController : Controller
    {
        private readonly BookStoreContext _context;

        public LoginController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: hiển thị form để nhập dữ liệu
        [HttpGet]
        public IActionResult Index()
        {
            // Đoc cookie "AdminEmail" neu co
            var savedEmail = Request.Cookies["AdminEmail"];

            if (!string.IsNullOrEmpty(savedEmail))
            {
                // Tự động điền email + tick Remember
                var model = new Login
                {
                    Email = savedEmail,
                    Remember = true
                };
                return View(model);
            }

            return View();
        }

        // POST: khi submit form
        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var pass = GetSHA26Hash(model.Password);

            var dataLogin = _context.AdminUsers
                .Where(x => x.Email.Equals(model.Email) && x.Password.Equals(pass))
                .FirstOrDefault();

            if (dataLogin != null)
            {
                // Lưu Session
                HttpContext.Session.SetString("AdminLogin", model.Email);
                HttpContext.Session.SetString("AdminName", dataLogin.Name ?? "Admin");

                // Xử lý Remember Me — ghi Cookie
                if (model.Remember)
                {
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),   // Hết hạn sau 7 ngày
                        HttpOnly = true,                     // JS không đọc được (bảo mật)
                        IsEssential = true
                    };
                    Response.Cookies.Append("AdminEmail", model.Email, options);
                }
                else
                {
                    // Nếu KHÔNG tick Remember → xóa cookie cũ (nếu có)
                    Response.Cookies.Delete("AdminEmail");
                }

                return RedirectToAction("Index", "Dashboard", new { area = "Admins" });
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng";
            return View(model);
        }

        // Logout
        [HttpGet]
        public IActionResult Logout()
        {
            // Xóa session
            HttpContext.Session.Remove("AdminLogin");
            HttpContext.Session.Remove("AdminName");

            // Xóa cookie
            Response.Cookies.Delete("AdminEmail");

            return RedirectToAction("Index");
        }

        // Mã hóa SHA256
        static string GetSHA26Hash(string input)
        {
            string hash = "";
            using (var sha256 = new SHA256Managed())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }
    }
}