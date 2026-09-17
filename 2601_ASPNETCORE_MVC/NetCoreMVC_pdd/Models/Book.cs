using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_NetCoreMVC_pdd.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Summary { get; set; }

        public List<Book> GetBookList()
        {
            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Chí Phèo",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b1.png",
                    Price = 500000,
                    Summary = "",
                    TotalPage = 250
                },
                new Book()
                {
                    Id = 2,
                    Title = "Lão Hạc",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b2.png",
                    Price = 450000,
                    Summary = "",
                    TotalPage = 220
                },
                new Book()
                {
                    Id = 3,
                    Title = "Số Đỏ",
                    AuthorId = 2,
                    GenreId = 2,
                    Image = "/images/products/b3.png",
                    Price = 600000,
                    Summary = "",
                    TotalPage = 300
                },
                new Book()
                {
                    Id = 4,
                    Title = "Tắt Đèn",
                    AuthorId = 2,
                    GenreId = 2,
                    Image = "/images/products/b4.png",
                    Price = 550000,
                    Summary = "",
                    TotalPage = 280
                }
            };

            return books;
        }

        public Book GetBookById(int id)
        {
            Book book = this.GetBookList().FirstOrDefault(b => b.Id == id);
            return book;
        }

        public List<SelectListItem> Authors { get; } = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Nam cao" },
                new SelectListItem { Value = "2", Text = "Ngô Tất Tố" },
                new SelectListItem { Value = "3", Text = "Adamkhoom" },
                new SelectListItem { Value = "4", Text = "Thiên sư Thích Nhất Hạnh" }
            };

        public List<SelectListItem> Genres { get; } = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Truyện tranh" },
                new SelectListItem { Value = "2", Text = "Văn học đường đại" },
                new SelectListItem { Value = "3", Text = "Phật học phổ thông" },
                new SelectListItem { Value = "4", Text = "Truyện cười" }
            };
    }
}
