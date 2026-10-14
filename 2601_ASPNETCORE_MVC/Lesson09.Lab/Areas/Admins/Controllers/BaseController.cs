using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson09.Lab.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BaseController : Controller, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra session "AdminLogin" — nếu chưa đăng nhập → chuyển về Login
            if (context.HttpContext.Session.GetString("AdminLogin") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        Controller = "Login",
                        Action = "Index",
                        Area = "Admins"
                    }));
            }
            base.OnActionExecuting(context);
        }
    }
}