using System.ComponentModel.DataAnnotations;

namespace Lesson09.Lab.LoginCus
{
    public class CustomerLogin
    {
        [Display(Name = "Email/Username")]
        [Required(ErrorMessage = "Vui lòng nhập Email hoặc Username")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }

    public class CustomerRegister
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Vui lòng nhập username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu từ 6 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}