using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;

namespace TrungTamQuanLiDT.ViewModel
{
    public class ChinhSuaThongTinHocVien
    {
        public int MaHocVien { get; set; } = 0;
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; } = DateTime.Now;
    }
}
