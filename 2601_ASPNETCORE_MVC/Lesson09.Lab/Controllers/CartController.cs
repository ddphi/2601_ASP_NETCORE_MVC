using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Lesson09.Lab.Models.DevXuongMoc;
using System.Text.Json;

namespace Lesson09.Lab.Controllers
{
    public class CartController : Controller
    {
        private readonly XuongMocContext _context;
        private List<Cart> carts = new List<Cart>();

        public CartController(XuongMocContext context)
        {
            _context = context;
        }

        // Load giỏ hàng từ Session TRƯỚC mỗi action
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cartInSession = HttpContext.Session.GetString("My-Cart");

            if (!string.IsNullOrEmpty(cartInSession))
            {
                carts = JsonSerializer.Deserialize<List<Cart>>(cartInSession)
                        ?? new List<Cart>();
            }

            base.OnActionExecuting(context);
        }

        // Lưu giỏ hàng vào Session SAU mỗi action
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContext.Session.SetString("My-Cart", JsonSerializer.Serialize(carts));
            base.OnActionExecuted(context);
        }

        // GET: /Cart
        public IActionResult Index()
        {
            return View(carts);
        }

        // GET: /Cart/Add/8
        public IActionResult Add(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var item = carts.FirstOrDefault(c => c.ProductId == id);
            if (item == null)
            {
                carts.Add(new Cart
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Image = product.Image,
                    Price = product.PriceNew,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            return RedirectToAction("Index");
        }

        // GET: /Cart/Remove/8
        public IActionResult Remove(int id)
        {
            carts.RemoveAll(c => c.ProductId == id);
            return RedirectToAction("Index");
        }

        // POST: /Cart/Update
        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            var item = carts.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                if (quantity <= 0) carts.Remove(item);
                else item.Quantity = quantity;
            }
            return RedirectToAction("Index");
        }

        // GET: /Cart/Clear
        public IActionResult Clear()
        {
            carts.Clear();
            return RedirectToAction("Index");
        }
    }
}