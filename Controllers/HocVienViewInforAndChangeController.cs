using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrungTamQuanLiDT.Controllers
{
    public class HocVienViewInforAndChangeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocVienViewInforAndChangeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Hiển thị thông tin học viên
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return NotFound(); // Trả về NotFound nếu không có tên người dùng
            }

            var hocVien = await _context.HocViens.FirstOrDefaultAsync(u => u.TaiKhoan == userName);

            if (hocVien == null)
                return NotFound();

            return View(hocVien);
        }

        // Xử lý chỉnh sửa thông tin
        public async Task<IActionResult> Edit()
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
                return NotFound();

            var hocVien = await _context.HocViens.FirstOrDefaultAsync(u => u.TaiKhoan == userName);

            if (hocVien == null)
                return NotFound();

            return View(hocVien);
        }

        // Xử lý cập nhật
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoTen,Email,SDT,NgaySinh")] UserModel hocVien)
        {
            if (!ModelState.IsValid)
            {
                return View(hocVien);
            }

            var hocVienDb = await _context.HocViens.FirstOrDefaultAsync(h => h.MaHocVien == id);

            if (hocVienDb == null)
            {
                return NotFound();
            }

            // Chỉ cập nhật các trường người dùng được phép sửa
            hocVienDb.HoTen = hocVien.HoTen;
            hocVienDb.Email = hocVien.Email;
            hocVienDb.SDT = hocVien.SDT;
            hocVienDb.NgaySinh = hocVien.NgaySinh;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocVienExists(id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction("Index", "HocVienViewInforAndChange");
        }

        private bool HocVienExists(int id)
        {
            return _context.HocViens.Any(e => e.MaHocVien == id);
        }
    }
}

