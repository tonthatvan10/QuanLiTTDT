using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Models;
using TrungTamQuanLiDT.Data;
using X.PagedList; // Thêm namespace này
using X.PagedList.Extensions;

namespace TrungTamQuanLiDT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "HocVien")]
        public IActionResult Privacy(string searchString, string giangVien, DateTime? fromDate, DateTime? toDate, int page = 1)
        {
            int pageSize = 3;
            var query = _context.KhoaHocs.AsQueryable();

            // Lọc theo tên khóa học
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(k => k.TenKhoaHoc.Contains(searchString));
            }

            // Lọc theo giảng viên
            if (!string.IsNullOrEmpty(giangVien))
            {
                query = query.Where(k => k.GiangVien == giangVien);
            }

            // Lọc theo khoảng thời gian khai giảng
            if (fromDate.HasValue)
            {
                query = query.Where(k => k.ThoiGianKhaiGiang >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(k => k.ThoiGianKhaiGiang <= toDate.Value);
            }

            // Truyền danh sách giảng viên duy nhất về View
            ViewBag.GiangViens = _context.KhoaHocs
                .Where(k => !string.IsNullOrEmpty(k.GiangVien))
                .Select(k => k.GiangVien)
                .Distinct()
                .ToList();

            // Lưu trạng thái bộ lọc
            ViewBag.SearchString = searchString;
            ViewBag.GiangVienSelected = giangVien;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            // Lấy danh sách khóa học phân trang
            var pagedList = query.OrderBy(k => k.ThoiGianKhaiGiang).ToPagedList(page, pageSize);

            // Kiểm tra danh sách rỗng
            if (!pagedList.Any())
            {
                ViewBag.Message = "Không tìm thấy khóa học nào phù hợp.";
            }

            return View(pagedList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}