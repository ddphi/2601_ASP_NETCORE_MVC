using System;
using System.ComponentModel.DataAnnotations;

namespace Lesson05_DemoDataValidation.Models
{
    public class Member_Update
    {
        public string MemberId { get; set; } = string.Empty;

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [MinLength(4, ErrorMessage = "Tên đăng nhập phải dài hơn 3 ký tự")]
        public string UserName { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Họ tên từ 3-50 ký tự")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "SĐT phải có định dạng XXX-XXX-XXXX")]
        public string Phone { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải từ 6 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Chọn ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}