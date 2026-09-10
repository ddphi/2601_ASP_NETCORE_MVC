using Microsoft.AspNetCore.Mvc;
using _98lesson2.Models;

namespace _98lesson2.Controllers
{
    // Route cho toàn bộ controller: tất cả action sẽ có prefix /san-pham
    [Route("san-pham")]
    public class ProductController : Controller
    {
        // URL: /san-pham (hoặc /san-pham/index)
        [Route("")]          // ← route rỗng, tức là /san-pham
        [Route("index")]     // ← cũng cho phép /san-pham/index
        public IActionResult Index()
        {
            ViewBag.message = "Du lieu truyen thong qua viewbag";
            ViewData["messageView"] = "Du lieu truyen thong qua viewdata";
            TempData["messageTemp"] = "Du lieu truyen thong qua tempdata";
            return View();
        }

        // URL: /san-pham/chi-tiet
        [Route("chi-tiet")]
        public IActionResult GetProduct()
        {
            Product p = new Product()
            {
                Id = 1,
                Name = "Iphone 14 Pro Max",
                Price = 30000000,
                Year = 2022
            };
            ViewBag.Product = p;
            ViewData["Product"] = p;
            TempData["Product"] = p;
            return View(p);
        }

        // URL: /san-pham/tat-ca
        [Route("tat-ca")]
        public IActionResult GetAllProduct()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){Id=1, Name="Iphone 14 Pro Max", Price=30000000, Year=2022},
                new Product(){Id=2, Name="Iphone 13 Pro Max", Price=25000000, Year=2021},
                new Product(){Id=3, Name="Iphone 12 Pro Max", Price=20000000, Year=2020},
                new Product(){Id=4, Name="Iphone 11 Pro Max", Price=15000000, Year=2019},
                new Product(){Id=5, Name="Iphone X", Price=10000000, Year=2018}
            };
            ViewBag.Products = products;
            return View();
        }

        // URL: /san-pham/tim-kiem?name=xxx
        [Route("tim-kiem")]
        public IActionResult Search(string name)
        {
            List<Product> products = new List<Product>()
            {
                new Product(){Id=1, Name="Iphone 14 Pro Max", Price=30000000, Year=2022},
                new Product(){Id=2, Name="Iphone 13 Pro Max", Price=25000000, Year=2021},
                new Product(){Id=3, Name="Iphone 12 Pro Max", Price=20000000, Year=2020},
                new Product(){Id=4, Name="Iphone 11 Pro Max", Price=15000000, Year=2019},
                new Product(){Id=5, Name="Iphone X", Price=10000000, Year=2018}
            };

            List<Product> prosearch = products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            ViewBag.Products = prosearch;
            return View();
        }
    }
}