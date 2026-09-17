using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4_pdd.Models
{
    public class DataLocal
    {
        public static List<People> _people = new List<People>()
        {
            new People() { Id = 0, Name = "Devmaster", Email = "devmaster.edu.vn@gmail.com",
                Phone = "0978611889", Address = "25 Vũ Ngọc Phan",
                Avatar = "/images/avatar/00.png", Birthday = Convert.ToDateTime("2012/09/22"),
                Bio = "Viện Công Nghệ Devmaster", Gender = 0 },

            new People() { Id = 1, Name = "Trinh Van Chung", Email = "chungtrinhj@gmail.com",
                Phone = "0978611889", Address = "25 Vũ Ngọc Phan",
                Avatar = "/images/avatar/01.jpg", Birthday = Convert.ToDateTime("1979/05/25"),
                Bio = "Devmaster Academy", Gender = 1 },

            new People() { Id = 2, Name = "Nguyễn Huy", Email = "huynhnguyen@gmail.com",
                Phone = "0912113113", Address = "Gia lâm, hà nội",
                Avatar = "/images/avatar/02.jpg", Birthday = Convert.ToDateTime("1999/02/12"),
                Bio = "Viện Devmaster", Gender = 1 },

            new People() { Id = 3, Name = "Tiểu Long Nữ", Email = "longnutieu@gmail.com",
                Phone = "0904001002", Address = "Ba đình, hà nội",
                Avatar = "/images/avatar/03.jpg", Birthday = Convert.ToDateTime("2000/02/02"),
                Bio = "Nhân vật trong phim kiếm hiệp", Gender = 2 },

            new People() { Id = 4, Name = "Pikachu", Email = "chupika@gmail.com",
                Phone = "0902114115", Address = "Quảng trung, hà đông",
                Avatar = "/images/avatar/04.jpg", Birthday = Convert.ToDateTime("2001/01/01"),
                Bio = "Hoạt hình Nhật Bản", Gender = 0 }
        };

        public static List<People> GetPeoples()
        {
            return _people;
        }

        public static People? GetPeopleById(int Id)
        {
            var people = _people.FirstOrDefault(x => x.Id == Id);
            return people;
        }
    }
}