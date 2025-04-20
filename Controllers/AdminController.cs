using Microsoft.AspNetCore.Mvc;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TrungTamQuanLiDT.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            var model = new AdminDashboardViewModel
            {
                SoLuongKhoaHoc = await _context.KhoaHocs.CountAsync(),
                SoLuongNguoiDung = await _context.HocViens.CountAsync(),
            };

            return View(model);
        }
    }
}
