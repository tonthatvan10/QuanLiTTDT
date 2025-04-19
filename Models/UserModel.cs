using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;



namespace TrungTamQuanLiDT.Models
{
    public class UserModel
    {
        [Key] public int MaHocVien { get; set; }
        [Required] public string HoTen { get; set; } = string.Empty;
        [Required] public string TaiKhoan { get; set; } = string.Empty;
        [Required] public string MatKhau { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string SDT { get; set; } = string.Empty;
        [Required] public DateTime NgaySinh{ get; set; }
        [Required] UserRole Role { get; set; } = UserRole.HocVien;

        public ICollection<DangKyKhoaHocModel> DangKyMonHocs { get; set; } = new List<DangKyKhoaHocModel>();

        //Enum
        public enum UserRole
        {
            Admin,
            HocVien
        }

    }
}
