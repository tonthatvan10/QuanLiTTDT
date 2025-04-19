using System.ComponentModel.DataAnnotations;    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrungTamQuanLiDT.Models
{
    public class DangKyKhoaHocModel
    {
        [Key] public int MaDangKy { get; set; }
        [ForeignKey("KhoaHoc")] public int MaKhoaHoc { get; set; }
        [ForeignKey("HocVien")] public int MaHocVien { get; set; }
        [Required] public DateTime NgayDangKy { get; set; }
        public bool TrangThai { get; set; }

        //Navigation properties
        public KhoaHocModel KhoaHoc { get; set; } = new KhoaHocModel();
        public UserModel HocVien { get; set; } = new UserModel();
    }
}
