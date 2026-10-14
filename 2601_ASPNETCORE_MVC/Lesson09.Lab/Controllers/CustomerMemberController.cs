using Lesson09.Lab.LoginCus;
using Lesson09.Lab.Models.DevXuongMoc;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Lesson09.Lab.Controllers
{
    public class CustomerMemberController : Controller
    {
        private readonly XuongMocContext _context;

        public CustomerMemberController(XuongMocContext context)
        {
            _context = context;
        }

        // GET: /CustomerMember
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /CustomerMember/Login
        [HttpPost]
        public IActionResult Login(CustomerLogin model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View("Index");
            }

            var pass = GetSha256Hash(model.Password);

            var customer = _context.Customers
                .FirstOrDefault(c => (c.Email == model.Email || c.Username == model.Email)
                                     && c.Password == pass
                                     && c.Isdelete != 1);

            if (customer == null)
            {
                ViewBag.Error = "Email/Username hoặc mật khẩu không đúng";
                return View("Index");
            }

            // Lưu Session
            HttpContext.Session.SetInt32("CustomerId", (int)customer.Id);
            HttpContext.Session.SetString("CustomerName", customer.Name ?? "Khách");
            HttpContext.Session.SetString("CustomerEmail", customer.Email ?? "");

            return RedirectToAction("Index", "Product");
        }

        // POST: /CustomerMember/Register
        [HttpPost]
        public IActionResult Register(CustomerRegister model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng kiểm tra thông tin đăng ký";
                return View("Index");
            }

            // Kiểm tra trùng username/email
            if (_context.Customers.Any(c => c.Username == model.Username))
            {
                ViewBag.Error = "Username đã tồn tại";
                return View("Index");
            }
            if (_context.Customers.Any(c => c.Email == model.Email))
            {
                ViewBag.Error = "Email đã tồn tại";
                return View("Index");
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
            return View("Index");
        }

        // GET: /CustomerMember/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Remove("CustomerName");
            HttpContext.Session.Remove("CustomerEmail");
            return RedirectToAction("Index");
        }

        // Hash SHA256
        static string GetSha256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}