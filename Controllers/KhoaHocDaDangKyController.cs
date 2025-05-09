﻿using Microsoft.AspNetCore.Mvc;
using TrungTamQuanLiDT.ViewModel;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TrungTamQuanLiDT.Controllers
{
    public class KhoaHocDaDangKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhoaHocDaDangKyController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "HocVien")]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem khóa học đã đăng ký.";
                return RedirectToAction("Index", "Home");
            }

            var hocVien = await _context.HocViens.FirstOrDefaultAsync(h => h.TaiKhoan == username);
            if (hocVien == null)
            {
                TempData["Message"] = "Không tìm thấy thông tin học viên.";
                return RedirectToAction("Index", "Home");
            }

            var danhSachDangKy = await _context.DangKyHocs
                .Include(dk => dk.KhoaHoc)
                .Where(dk => dk.MaHocVien == hocVien.MaHocVien &&
                             dk.TrangThai != DangKyKhoaHocModel.TrangThaiDangKy.DaHuy &&
                             dk.KhoaHoc.ThoiGianKetThuc > DateTime.Now)
                .ToListAsync();

            return View(danhSachDangKy);

        }

        [Authorize(Roles = "HocVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyDangKy(int maDangKy)
        {
            var dangKy = await _context.DangKyHocs.FindAsync(maDangKy);
            if (dangKy == null)
            {
                TempData["Message"] = "Không tìm thấy đăng ký.";
                return RedirectToAction("Index");
            }

            var username = User.Identity?.Name;
            var hocVien = _context.HocViens.FirstOrDefault(h => h.TaiKhoan == username);
            if (hocVien == null || dangKy.MaHocVien != hocVien.MaHocVien)
            {
                TempData["Message"] = "Bạn không có quyền hủy đăng ký này.";
                return RedirectToAction("Index");
            }

            dangKy.TrangThai = DangKyKhoaHocModel.TrangThaiDangKy.DaHuy;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Hủy đăng ký thành công!";
            return RedirectToAction("Index");
        }

    }
}
