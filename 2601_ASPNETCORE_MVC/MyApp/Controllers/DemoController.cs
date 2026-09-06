using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
