using Microsoft.AspNetCore.Mvc;
using MyAppMVC.Models;
using System;
using System.Collections.Generic;

namespace MyAppMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    Id = 1, Name = "Hoàng Anh",
                    Email = "anh@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/02.jpg"),
                    Gender = 1, Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account()
                {
                    Id = 1, Name = "Trương Giang",
                    Email = "giang@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/03.jpg"),
                    Gender = 1, Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account()
                {
                    Id = 1, Name = "Hoàng Thúy",
                    Email = "thuy@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/04.jpg"),
                    Gender = 1, Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
            };
            ViewBag.Accounts = accounts;
            return View();
        }

        // định nghĩa url và name cho action
        [Route("ho-so-cua-toi", Name = "profile")]
        public IActionResult Profile(int id)
        {
            // Danh sách Account như trên Action Index
            List<Account> accounts = new List<Account>
    {
        new Account() { Id = 1, Name = "Hoàng Anh", Email = "anh@gmail.com",
            Phone = "0986456789", Address = "Hà Nội",
            Avatar = "/images/Avatar/01.png",
            Gender = 1, Bio = "My name is small",
            Birthday = new DateTime(1998, 7, 15) },

        new Account() { Id = 2, Name = "Trương Giang", Email = "giang@gmail.com",
            Phone = "0986456789", Address = "Hà Nội",
            Avatar = "/images/Avatar/02.png",
            Gender = 1, Bio = "My name is small",
            Birthday = new DateTime(1998, 7, 15) },

        new Account() { Id = 3, Name = "Hoàng Thúy", Email = "thuy@gmail.com",
            Phone = "0986456789", Address = "Hà Nội",
            Avatar = "/images/Avatar/03.png",
            Gender = 1, Bio = "My name is small",
            Birthday = new DateTime(1998, 7, 15) },
    };
            // sử dụng using System.Linq; truy xuất dữ liệu 1 đối tượng trong danh sách theo id
            Account account = accounts.FirstOrDefault(ac => ac.Id == id);
            // gửi đối tượng account qua view
            ViewBag.account = account;
            return View();
        }
    }
}