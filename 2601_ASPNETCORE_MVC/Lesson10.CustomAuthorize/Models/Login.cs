using System.ComponentModel.DataAnnotations;
namespace Lesson10.CustomAuthorize.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email không để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}