using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrungTamQuanLiDT.Attributes;

namespace TrungTamQuanLiDT.Models
{
    public class UserModel
    {
        [Key]
        public int MaHocVien { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string TaiKhoan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [PasswordComplexity]
        public string MatKhau { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SDT { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public UserRole Role { get; set; } = UserRole.HocVien;

        public ICollection<DangKyKhoaHocModel> DangKyHocs { get; set; } = new List<DangKyKhoaHocModel>();

        // Enum định nghĩa vai trò người dùng
        public enum UserRole
        {
            Admin,
            HocVien
        }
    }
}
