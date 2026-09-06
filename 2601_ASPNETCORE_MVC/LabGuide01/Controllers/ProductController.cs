using Microsoft.AspNetCore.Mvc;

namespace LabGuide01.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
