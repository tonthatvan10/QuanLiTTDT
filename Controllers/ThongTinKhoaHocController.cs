using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using static TrungTamQuanLiDT.Models.DangKyKhoaHocModel;

namespace TrungTamQuanLiDT.Controllers
{
    public class ThongTinKhoaHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongTinKhoaHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem chi tiết khóa học.";
                return RedirectToAction("Login", "Authentication");
            }

            var khoaHoc = _context.KhoaHocs
                .Include(k => k.DangKyHocs)
                .FirstOrDefault(k => k.MaKhoaHoc == id);

            if (khoaHoc == null)
            {
                return NotFound();
            }

            // Tính số lượng học viên đã đăng ký (trạng thái không phải Đã Hủy)
            var soLuongDaDangKy = khoaHoc.DangKyHocs
                .Count(d => d.TrangThai != TrangThaiDangKy.DaHuy);
            ViewBag.SoLuongDaDangKy = soLuongDaDangKy;

            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(int khoaHocId)
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                TempData["Message"] = "Bạn cần đăng nhập để đăng ký khóa học.";
                return RedirectToAction("Details", new { id = khoaHocId });
            }

            var hocVien = _context.HocViens.FirstOrDefault(hv => hv.TaiKhoan == userName);
            if (hocVien == null)
            {
                TempData["Message"] = "Không tìm thấy thông tin học viên.";
                return RedirectToAction("Details", new { id = khoaHocId });
            }

            int userId = hocVien.MaHocVien;

            var khoaHoc = _context.KhoaHocs
                .Include(k => k.DangKyHocs)
                .FirstOrDefault(k => k.MaKhoaHoc == khoaHocId);

            if (khoaHoc == null)
            {
                return NotFound();
            }

            if (khoaHoc.ThoiGianKhaiGiang <= DateTime.Now)
            {
                TempData["Message"] = "Khóa học này đã khai giảng, bạn không thể đăng ký.";
                return RedirectToAction("Details", new { id = khoaHocId });
            }

            var soLuongDaDangKy = khoaHoc.DangKyHocs
                .Count(d => d.TrangThai != TrangThaiDangKy.DaHuy);

            if (soLuongDaDangKy >= khoaHoc.SoLuongHocVienToiDa)
            {
                TempData["Message"] = "Khóa học này đã đủ số lượng học viên.";
                return RedirectToAction("Details", new { id = khoaHocId });
            }

            var daDangKy = khoaHoc.DangKyHocs
                .Any(d => d.MaHocVien == userId && d.TrangThai != TrangThaiDangKy.DaHuy);

            if (daDangKy)
            {
                TempData["Message"] = "Bạn đã đăng ký khóa học này.";
                return RedirectToAction("Details", new { id = khoaHocId });
            }

            var dangKy = new DangKyKhoaHocModel
            {
                MaHocVien = userId,
                MaKhoaHoc = khoaHocId,
                NgayDangKy = DateTime.Now,
                TrangThai = TrangThaiDangKy.DangCho
            };

            _context.DangKyHocs.Add(dangKy);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Đăng ký thành công!";
            return RedirectToAction("Privacy", "Home");
        }
    }
}
