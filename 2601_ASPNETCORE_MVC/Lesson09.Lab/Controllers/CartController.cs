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

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContext.Session.SetString("My-Cart", JsonSerializer.Serialize(carts));
            base.OnActionExecuted(context);
        }

        public IActionResult Index()
        {
            return View(carts);
        }

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

        public IActionResult Remove(int id)
        {
            carts.RemoveAll(c => c.ProductId == id);
            return RedirectToAction("Index");
        }

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

        public IActionResult Clear()
        {
            carts.Clear();
            return RedirectToAction("Index");
        }


        public IActionResult Orders()
        {
            if (HttpContext.Session.GetInt32("CustomerId") == null)
            {
                return Redirect("/CustomerMember/Index?url=/Cart/Orders");
            }

            if (carts.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var customerId = HttpContext.Session.GetInt32("CustomerId");
            var customer = _context.Customers.FirstOrDefault(c => c.Id == customerId);
            ViewBag.Customer = customer;

            ViewBag.Total = carts.Sum(x => x.Price * x.Quantity);

            var payments = _context.PaymentMethods.ToList();
            ViewData["IdPayment"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(payments, "Id", "Name");

            return View(carts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Orders(string name, string email, string phone,
                                    string address, string notes, long idPayment)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return Redirect("/CustomerMember/Index?url=/Cart/Orders");
            }

            if (carts.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                Idorders = DateTime.Now.ToString("yyyyMMddHH"), 
                OrdersDate = DateTime.Now,
                Idcustomer = customerId,
                Idpayment = idPayment,
                TotalMoney = carts.Sum(x => x.Price * x.Quantity),
                Notes = notes,
                NameReciver = name,
                Address = address,
                Email = email,
                Phone = phone,
                Isdelete = 0,
                Isactive = 1
            };

            _context.Orders.Add(order);
            _context.SaveChanges(); 

            foreach (var item in carts)
            {
                var detail = new OrdersDetail
                {
                    Idord = order.Id,                  
                    Idproduct = item.ProductId,
                    Price = item.Price,
                    Qty = item.Quantity,
                    Total = item.Price * item.Quantity,
                    ReturnQty = 0
                };
                _context.OrdersDetails.Add(detail);
            }
            _context.SaveChanges();

            carts.Clear();
            HttpContext.Session.SetString("My-Cart", "[]");

            TempData["Success"] = $"Đặt hàng thành công! Mã đơn: {order.Idorders}";

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}