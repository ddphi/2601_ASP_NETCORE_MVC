using System.ComponentModel.DataAnnotations;

namespace Lesson10.CustomAuthorize.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu từ 6 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}