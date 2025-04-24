using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrungTamQuanLiDT.Models
{
    public class KhoaHocModel
    {
        [Key]
        public int MaKhoaHoc { get; set; }

        [Required(ErrorMessage = "Tên khóa học không được để trống")]
        public string TenKhoaHoc { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên giảng viên không được để trống")]
        public string GiangVien { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thời gian khai giảng không được để trống")]
        [DataType(DataType.Date)]
        public DateTime ThoiGianKhaiGiang { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống")]
        [DataType(DataType.Date)]
        public DateTime ThoiGianKetThuc { get; set; }

        [Required(ErrorMessage = "Học phí không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Học phí phải lớn hơn 0")]
        public decimal HocPhi { get; set; }

        [Required(ErrorMessage = "Số lượng học viên tối đa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng học viên phải lớn hơn 0")]
        public int SoLuongHocVienToiDa { get; set; }

        public ICollection<DangKyKhoaHocModel> DangKyHocs { get; set; } = new List<DangKyKhoaHocModel>();
    }
}
