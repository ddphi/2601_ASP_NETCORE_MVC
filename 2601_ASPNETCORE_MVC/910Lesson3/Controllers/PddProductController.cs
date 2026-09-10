using Microsoft.AspNetCore.Mvc;

namespace _910Lesson3.Controllers
{
    public class PddProductController : Controller
    {
        public IActionResult Index(int? pid)
        {
            ViewBag.pid = pid;
            return View();
        }
    }
}
