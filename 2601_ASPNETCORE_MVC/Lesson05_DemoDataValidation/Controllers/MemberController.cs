using Microsoft.AspNetCore.Mvc;
using Lesson05_DemoDataValidation.Models;

namespace Lesson05_DemoDataValidation.Controllers
{
    public class MemberController : Controller
    {
        public static List<Member_Update> members = new List<Member_Update>()
        {
            new Member_Update
            {
                MemberId = "M001",
                UserName = "john_doe",
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890",
                Password = "password123",
                Birthday = new DateTime(1990, 1, 1)
            }
        };

        public IActionResult Index()
        {
            return View(members);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member_Update member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            member.MemberId = Guid.NewGuid().ToString();
            members.Add(member);
            return RedirectToAction("Index");
        }
    }
}