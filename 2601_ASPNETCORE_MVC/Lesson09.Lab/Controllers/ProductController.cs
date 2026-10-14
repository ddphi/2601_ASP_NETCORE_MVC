using Microsoft.AspNetCore.Mvc;
using Lesson09.Lab.Models.DevXuongMoc;

namespace Lesson09.Lab.Controllers
{
    public class ProductController : Controller
    {
        private readonly XuongMocContext _context;

        public ProductController(XuongMocContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Where(p => p.Isdelete == false || p.Isdelete == null)
                .ToList();

            return View(products);
        }
    }
}