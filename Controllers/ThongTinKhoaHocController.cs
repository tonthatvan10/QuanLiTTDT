using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using static TrungTamQuanLiDT.Models.DangKyKhoaHocModel;

namespace TrungTamQuanLiDT.Controllers
{
    [Authorize]
    public class ThongTinKhoaHocController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ThongTinKhoaHocController> _logger;

        public ThongTinKhoaHocController(ApplicationDbContext context, ILogger<ThongTinKhoaHocController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Phương thức tiện ích để lấy MaHocVien từ claims
        private IActionResult TryGetMaHocVien(out int maHocVien)
        {
            maHocVien = 0;
            if (!User.Identity.IsAuthenticated || !User.IsInRole(UserModel.UserRole.HocVien.ToString()))
            {
                return RedirectToAction("Login", "Authentication");
            }

            if (!int.TryParse(User.FindFirst("MaHocVien")?.Value, out maHocVien))
            {
                _logger.LogError("Không thể lấy MaHocVien từ claims.");
                TempData["Error"] = "Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.";
                return RedirectToAction("Login", "Authentication");
            }

            return null;
        }

        // GET: /ThongTinKhoaHoc/Details/5
        /// <summary>
        /// Hiển thị chi tiết khóa học và trạng thái đăng ký của học viên.
        /// </summary>
        /// <param name="id">Mã khóa học</param>
        /// <returns>View chứa thông tin khóa học</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Mã khóa học không hợp lệ: {id}", id);
                return BadRequest("Mã khóa học không hợp lệ.");
            }

            var khoaHoc = await _context.KhoaHocs
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.MaKhoaHoc == id);

            if (khoaHoc == null)
            {
                _logger.LogWarning("Không tìm thấy khóa học với MaKhoaHoc: {id}", id);
                return NotFound();
            }

            bool isRegistered = false;
            if (User.Identity.IsAuthenticated && User.IsInRole(UserModel.UserRole.HocVien.ToString()))
            {
                var redirectResult = TryGetMaHocVien(out int maHocVien);
                if (redirectResult != null)
                {
                    return redirectResult;
                }

                isRegistered = await _context.DangKyHocs
                    .AnyAsync(d => d.MaKhoaHoc == id && d.MaHocVien == maHocVien && d.TrangThai != TrangThaiDangKy.DaHuy);
            }

            int soLuongDangKyHienTai = await _context.DangKyHocs
                .CountAsync(d => d.MaKhoaHoc == id && d.TrangThai != TrangThaiDangKy.DaHuy);

            ViewBag.IsRegistered = isRegistered;
            ViewBag.CanRegister = khoaHoc.ThoiGianKhaiGiang > DateTime.Now &&
                                 soLuongDangKyHienTai < khoaHoc.SoLuongHocVienToiDa;
            ViewBag.SoLuongDangKyHienTai = soLuongDangKyHienTai;

            return View(khoaHoc);
        }

        // POST: /ThongTinKhoaHoc/RegisterCourse/5
        /// <summary>
        /// Đăng ký khóa học cho học viên.
        /// </summary>
        /// <param name="id">Mã khóa học</param>
        /// <returns>Chuyển hướng về trang chi tiết khóa học</returns>
        [Authorize(Roles = nameof(UserModel.UserRole.HocVien))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCourse([Bind("MaKhoaHoc")] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Mã khóa học không hợp lệ: {id}", id);
                return BadRequest("Mã khóa học không hợp lệ.");
            }

            var khoaHoc = await _context.KhoaHocs
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.MaKhoaHoc == id);

            if (khoaHoc == null)
            {
                _logger.LogWarning("Không tìm thấy khóa học với MaKhoaHoc: {id}", id);
                return NotFound();
            }

            if (khoaHoc.ThoiGianKhaiGiang <= DateTime.Now)
            {
                TempData["Error"] = "Khóa học đã khai giảng, không thể đăng ký.";
                return RedirectToAction("Details", new { id });
            }

            int soLuongDangKyHienTai = await _context.DangKyHocs
                .CountAsync(d => d.MaKhoaHoc == id && d.TrangThai != TrangThaiDangKy.DaHuy);

            if (soLuongDangKyHienTai >= khoaHoc.SoLuongHocVienToiDa)
            {
                TempData["Error"] = "Khóa học đã đủ số lượng học viên.";
                return RedirectToAction("Details", new { id });
            }

            var redirectResult = TryGetMaHocVien(out int maHocVien);
            if (redirectResult != null)
            {
                return redirectResult;
            }

            if (await _context.DangKyHocs
                .AnyAsync(d => d.MaKhoaHoc == id && d.MaHocVien == maHocVien && d.TrangThai != TrangThaiDangKy.DaHuy))
            {
                TempData["Error"] = "Bạn đã đăng ký khóa học này rồi.";
                return RedirectToAction("Details", new { id });
            }

            var dangKy = new DangKyKhoaHocModel
            {
                MaKhoaHoc = id,
                MaHocVien = maHocVien,
                NgayDangKy = DateTime.Now,
                TrangThai = TrangThaiDangKy.DangCho
            };

            try
            {
                await _context.DangKyHocs.AddAsync(dangKy);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đăng ký khóa học thành công! Vui lòng chờ xác nhận.";
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Lỗi cơ sở dữ liệu khi đăng ký khóa học với MaKhoaHoc: {id}", id);
                TempData["Error"] = "Lỗi cơ sở dữ liệu khi đăng ký. Vui lòng thử lại.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không xác định khi đăng ký khóa học với MaKhoaHoc: {id}", id);
                TempData["Error"] = "Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại sau.";
            }

            return RedirectToAction("Details", new { id });
        }

        // POST: /ThongTinKhoaHoc/CancelRegistration/5
        /// <summary>
        /// Hủy đăng ký khóa học của học viên.
        /// </summary>
        /// <param name="id">Mã khóa học</param>
        /// <returns>Chuyển hướng về trang chi tiết khóa học</returns>
        [Authorize(Roles = nameof(UserModel.UserRole.HocVien))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelRegistration([Bind("MaKhoaHoc")] int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Mã khóa học không hợp lệ: {id}", id);
                return BadRequest("Mã khóa học không hợp lệ.");
            }

            var khoaHoc = await _context.KhoaHocs
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.MaKhoaHoc == id);

            if (khoaHoc == null)
            {
                _logger.LogWarning("Không tìm thấy khóa học với MaKhoaHoc: {id}", id);
                return NotFound();
            }

            if (khoaHoc.ThoiGianKhaiGiang <= DateTime.Now)
            {
                TempData["Error"] = $"Khóa học đã khai giảng vào {khoaHoc.ThoiGianKhaiGiang:dd/MM/yyyy HH:mm}, không thể hủy đăng ký.";
                return RedirectToAction("Details", new { id });
            }

            var redirectResult = TryGetMaHocVien(out int maHocVien);
            if (redirectResult != null)
            {
                return redirectResult;
            }

            var dangKy = await _context.DangKyHocs
                .FirstOrDefaultAsync(d => d.MaKhoaHoc == id && d.MaHocVien == maHocVien && d.TrangThai == TrangThaiDangKy.DangCho);

            if (dangKy == null)
            {
                TempData["Error"] = "Bạn chưa đăng ký khóa học này hoặc đăng ký không ở trạng thái chờ.";
                return RedirectToAction("Details", new { id });
            }

            try
            {
                dangKy.TrangThai = TrangThaiDangKy.DaHuy;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Hủy đăng ký khóa học thành công.";
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Lỗi cơ sở dữ liệu khi hủy đăng ký khóa học với MaKhoaHoc: {id}", id);
                TempData["Error"] = "Lỗi cơ sở dữ liệu khi hủy đăng ký. Vui lòng thử lại.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không xác định khi hủy đăng ký khóa học với MaKhoaHoc: {id}", id);
                TempData["Error"] = "Đã xảy ra lỗi khi hủy đăng ký. Vui lòng thử lại sau.";
            }

            return RedirectToAction("Details", new { id });
        }
    }
}