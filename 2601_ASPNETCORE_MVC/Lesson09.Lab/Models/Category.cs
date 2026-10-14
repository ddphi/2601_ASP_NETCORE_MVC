using System;
using System.Collections.Generic;

namespace Lesson09.Lab.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<Book> Books { get; set; } = new List<Book>();
    }
}