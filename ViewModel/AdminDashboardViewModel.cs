using Microsoft.AspNetCore.Mvc;

namespace TrungTamQuanLiDT.ViewModel
{
    public class AdminDashboardViewModel
    {
        public int TongSoHocVienDangKy { get; set; }
        public int TongSoKhoaHoc { get; set; }
        public Dictionary<string, int> ThongKeDangKyTheoKhoaHoc { get; set; } = new();
    }
}
