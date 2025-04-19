using System.ComponentModel.DataAnnotations;

namespace TrungTamQuanLiDT.ViewModel
{
    public class LoginViewModel
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string TaiKhoan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống"), DataType(DataType.Password)]
        public string MatKhau { get; set; } = string.Empty;

        [Display(Name = "Nhớ mật khẩu?")]
        public bool RememberMe { get; set; }
    }
}
