using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;

namespace TrungTamQuanLiDT.Controllers
{
    public class UserModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserModels
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 10)
        {
            var query = _context.HocViens.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(h => h.HoTen.Contains(searchString));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var hocViens = await query
                .OrderBy(h => h.HoTen)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;

            return View(hocViens);
        }

        // GET: UserModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.HocViens
                .FirstOrDefaultAsync(m => m.MaHocVien == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: UserModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoTen,TaiKhoan,MatKhau,Email,SDT,NgaySinh,Role")] UserModel userModel)
        {
            userModel.NgaySinh = userModel.NgaySinh == default ? DateTime.Now : userModel.NgaySinh;

            if (await _context.HocViens.AnyAsync(u => u.TaiKhoan == userModel.TaiKhoan))
            {
                ModelState.AddModelError("TaiKhoan", "Tài khoản này đã được sử dụng.");
            }

            if (await _context.HocViens.AnyAsync(u => u.MatKhau == userModel.MatKhau))
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu này đã tồn tại. Vui lòng chọn mật khẩu khác.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.HocViens.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHocVien,HoTen,TaiKhoan,MatKhau,Email,SDT,NgaySinh")] UserModel userModel)
        {
            if (id != userModel.MaHocVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.MaHocVien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.HocViens
                .FirstOrDefaultAsync(m => m.MaHocVien == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.HocViens.FindAsync(id);
            if (userModel != null)
            {
                // Xóa các bản đăng ký khóa học liên quan
                var dangKyList = _context.DangKyHocs
                                         .Where(dk => dk.MaHocVien == id);

                _context.DangKyHocs.RemoveRange(dangKyList);

                // Sau đó xóa học viên
                _context.HocViens.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool UserModelExists(int id)
        {
            return _context.HocViens.Any(e => e.MaHocVien == id);
        }
    }
}
