using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrungTamQuanLiDT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KhoaHocModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhoaHocModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KhoaHocModels
        public async Task<IActionResult> Index(string searchString, DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            var query = _context.KhoaHocs.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(k => k.TenKhoaHoc.Contains(searchString));
            }

            if (startDate.HasValue)
            {
                query = query.Where(k => k.ThoiGianKhaiGiang >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(k => k.ThoiGianKhaiGiang <= endDate.Value);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var khoaHocs = await query
                .OrderBy(k => k.ThoiGianKhaiGiang)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(khoaHocs);
        }

        // GET: KhoaHocModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaHocModel = await _context.KhoaHocs
                .FirstOrDefaultAsync(m => m.MaKhoaHoc == id);
            if (khoaHocModel == null)
            {
                return NotFound();
            }

            return View(khoaHocModel);
        }

        // GET: KhoaHocModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhoaHocModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhoaHoc,TenKhoaHoc,GiangVien,ThoiGianKhaiGiang,HocPhi,SoLuongHocVienToiDa")] KhoaHocModel khoaHocModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoaHocModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHocModel);
        }

        // GET: KhoaHocModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaHocModel = await _context.KhoaHocs.FindAsync(id);
            if (khoaHocModel == null)
            {
                return NotFound();
            }
            return View(khoaHocModel);
        }

        // POST: KhoaHocModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKhoaHoc,TenKhoaHoc,GiangVien,ThoiGianKhaiGiang,HocPhi,SoLuongHocVienToiDa")] KhoaHocModel khoaHocModel)
        {
            if (id != khoaHocModel.MaKhoaHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaHocModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaHocModelExists(khoaHocModel.MaKhoaHoc))
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
            return View(khoaHocModel);
        }

        // GET: KhoaHocModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaHocModel = await _context.KhoaHocs
                .FirstOrDefaultAsync(m => m.MaKhoaHoc == id);
            if (khoaHocModel == null)
            {
                return NotFound();
            }

            return View(khoaHocModel);
        }

        // POST: KhoaHocModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaHocModel = await _context.KhoaHocs.FindAsync(id);
            if (khoaHocModel != null)
            {
                var now = DateTime.Now;

                if (khoaHocModel.ThoiGianKhaiGiang > now || khoaHocModel.ThoiGianKetThuc < now)
                {
                    _context.KhoaHocs.Remove(khoaHocModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TempData["Error"] = "Không thể xóa khóa học đang diễn ra.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KhoaHocModelExists(int id)
        {
            return _context.KhoaHocs.Any(e => e.MaKhoaHoc == id);
        }
    }
}