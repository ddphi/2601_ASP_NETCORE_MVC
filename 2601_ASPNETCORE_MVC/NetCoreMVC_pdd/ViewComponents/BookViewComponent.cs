using Microsoft.AspNetCore.Mvc;
using Lab3_NetCoreMVC_pdd.Models;

namespace Lab3_NetCoreMVC_pdd.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        protected Book book = new Book();

        public IViewComponentResult Invoke()
        {
            var books = book.GetBookList();
            return View(books);
        }
    }
}