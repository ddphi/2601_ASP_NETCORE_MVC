using _910Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _910Lesson3.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int n = 0)
        {
            List<Category> categories = new List<Category>()
            {
                new Category { CategoryId = 1, CategoryName = "Điện tử" },
                new Category { CategoryId = 2, CategoryName = "Điện lạnh" },
                new Category { CategoryId = 3, CategoryName = "Đồ gia dụng" },
                new Category { CategoryId = 4, CategoryName = "Tiện ích" }
            };

            // n = 0 → trả về TẤT CẢ
            if (n == 0)
                return View(categories);

            var search = categories.Where(c => c.CategoryId > n);
            return View(search);
        }
    }
}