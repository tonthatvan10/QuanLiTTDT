using Microsoft.AspNetCore.Mvc;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TrungTamQuanLiDT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TongSoHocVienDangKy = _context.DangKyHocs.Count(),
                TongSoKhoaHoc = _context.KhoaHocs.Count(),
                ThongKeDangKyTheoKhoaHoc = _context.KhoaHocs
                    .Include(k => k.DangKyHocs)
                    .AsEnumerable() //LINQ in-memory
                    .GroupBy(k => string.IsNullOrWhiteSpace(k.TenKhoaHoc) ? "[Không rõ tên]" : k.TenKhoaHoc)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(k => k.DangKyHocs.Count)
                    )
            };

            return View(viewModel);
        }
    }
}
