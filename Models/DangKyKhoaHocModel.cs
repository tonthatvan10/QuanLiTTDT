using System.ComponentModel.DataAnnotations;    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrungTamQuanLiDT.Models
{
    public class DangKyKhoaHocModel
    {
        public enum TrangThaiDangKy
        {
            DangCho = 0, // Chờ xác nhận
            DaXacNhan = 1, // Đã xác nhận
            DaHuy = 2 // Đã hủy
        }
        [Key]
        public int MaDangKy { get; set; }
        [ForeignKey("KhoaHoc")] public int MaKhoaHoc { get; set; }
        [ForeignKey("HocVien")] public int MaHocVien { get; set; }
        [Required] public DateTime NgayDangKy { get; set; }
        public TrangThaiDangKy TrangThai { get; set; } = TrangThaiDangKy.DangCho;

        //Navigation properties
        public KhoaHocModel? KhoaHoc { get; set; }
        public UserModel? HocVien { get; set; }
    }
}
