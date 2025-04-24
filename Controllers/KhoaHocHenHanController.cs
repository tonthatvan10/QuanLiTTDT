using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using System.Linq;

namespace TrungTamQuanLiDT.Controllers
{
    [Authorize(Roles = "HocVien")]
    public class KhoaHocHetHanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhoaHocHetHanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem thông tin.";
                return RedirectToAction("Index", "Home");
            }

            var hocVien = await _context.HocViens.FirstOrDefaultAsync(h => h.TaiKhoan == username);
            if (hocVien == null)
            {
                TempData["Message"] = "Không tìm thấy học viên.";
                return RedirectToAction("Index", "Home");
            }

            var now = DateTime.Now;
            var khoaHocDaHetHan = await _context.DangKyHocs
                .Include(dk => dk.KhoaHoc)
                .Where(dk => dk.MaHocVien == hocVien.MaHocVien &&
                             dk.TrangThai != DangKyKhoaHocModel.TrangThaiDangKy.DaHuy &&
                             dk.KhoaHoc.ThoiGianKetThuc < now)
                .Select(dk => dk.KhoaHoc)
                .OrderByDescending(kh => kh.ThoiGianKetThuc)
                .ToListAsync();

            return View(khoaHocDaHetHan);
        }
    }
}
