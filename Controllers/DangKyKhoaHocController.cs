using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TrungTamQuanLiDT.Models;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using static TrungTamQuanLiDT.Models.DangKyKhoaHocModel;

namespace TrungTamQuanLiDT.Controllers
{
    public class DangKyKhoaHocController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DangKyKhoaHocController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /DangKyKhoaHoc/Index
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem danh sách đăng ký.";
                return RedirectToAction("Index", "Home");
            }

            var hocVien = await _context.HocViens.FirstOrDefaultAsync(h => h.TaiKhoan == username);
            if (hocVien == null)
            {
                TempData["Message"] = "Không tìm thấy thông tin học viên.";
                return RedirectToAction("Index", "Home");
            }

            var khoaHocDaDangKy = await _context.DangKyHocs
                .Include(dk => dk.KhoaHoc)
                .Where(dk => dk.MaHocVien == hocVien.MaHocVien &&
                             dk.TrangThai != DangKyKhoaHocModel.TrangThaiDangKy.DaHuy)
                .ToListAsync();

            return View(khoaHocDaDangKy);
        }


        // GET: DangKyKhoaHoc/Create
        public IActionResult Create()
        {
            ViewBag.HocVienList = new SelectList(_context.HocViens, "MaHocVien", "HoTen");
            ViewBag.KhoaHocList = new SelectList(_context.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc");
            return View();
        }

        // POST: /DangKyKhoaHoc/Create
        [HttpPost]
        public async Task<IActionResult> Create(DangKyKhoaHocModel model)
        {
            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.DangKyHocs)
                .FirstOrDefaultAsync(k => k.MaKhoaHoc == model.MaKhoaHoc);

            if (khoaHoc == null)
            {
                ModelState.AddModelError("", "Khóa học không tồn tại.");
                return View(model);
            }

            if (DateTime.Now >= khoaHoc.ThoiGianKhaiGiang)
            {
                ModelState.AddModelError("", "Khóa học đã khai giảng.");
                return View(model);
            }

            var soLuongDangKy = khoaHoc.DangKyHocs.Count();
            if (soLuongDangKy >= khoaHoc.SoLuongHocVienToiDa)
            {
                ModelState.AddModelError("", "Khóa học đã đủ số lượng học viên.");
                return View(model);
            }

            bool daDangKy = await _context.DangKyHocs
                .AnyAsync(d => d.MaHocVien == model.MaHocVien && d.MaKhoaHoc == model.MaKhoaHoc);

            if (daDangKy)
            {
                ModelState.AddModelError("", "Học viên đã đăng ký khóa học này.");
                return View(model);
            }

            model.NgayDangKy = DateTime.Now;
            model.TrangThai = TrangThaiDangKy.DangCho;

            _context.DangKyHocs.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "KhoaHocDaDangKy");
        }

    }
}
