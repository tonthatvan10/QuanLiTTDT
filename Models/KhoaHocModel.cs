using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace TrungTamQuanLiDT.Models
{
    public class KhoaHocModel
    {
        [Key] public int MaKhoaHoc { get; set; }

        [Required] public string TenKhoaHoc { get; set; } = string.Empty;
        [Required] public string GiangVien { get; set; } = string.Empty;
        [Required] public DateTime ThoiGianKhaiGiang { get; set; }
        [Required] public decimal HocPhi { get; set; }
        [Required] public int SoLuongHocVienToiDa { get; set; }

        public ICollection<DangKyKhoaHocModel> DangKyHocs { get; set; } = new List<DangKyKhoaHocModel>();
    }
}
