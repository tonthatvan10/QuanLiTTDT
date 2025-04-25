using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Models;
using TrungTamQuanLiDT.Data;
using X.PagedList;
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

        public IActionResult Privacy(string searchString, string giangVien, DateTime? fromDate, DateTime? toDate, int page = 1)
        {
            const int pageSize = 5;

            // Lọc danh sách khóa học
            var khoaHocs = FilterCourses(searchString, giangVien, fromDate, toDate)
                .OrderBy(k => k.ThoiGianKhaiGiang)
                .ToPagedList(page, pageSize);

            // Truyền danh sách giảng viên duy nhất cho dropdown
            ViewBag.GiangViens = GetDistinctGiangViens();

            // Lưu trạng thái bộ lọc cho giao diện
            ViewBag.SearchString = searchString;
            ViewBag.GiangVienSelected = giangVien;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            if (!khoaHocs.Any())
            {
                ViewBag.Message = "Không tìm thấy khóa học nào phù hợp.";
            }

            return View(khoaHocs);
        }

        private IQueryable<KhoaHocModel> FilterCourses(string searchString, string giangVien, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.KhoaHocs.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
                query = query.Where(k => k.TenKhoaHoc.Contains(searchString));

            if (!string.IsNullOrWhiteSpace(giangVien))
                query = query.Where(k => k.GiangVien == giangVien);

            if (fromDate.HasValue)
                query = query.Where(k => k.ThoiGianKhaiGiang >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(k => k.ThoiGianKhaiGiang <= toDate.Value);

            return query;
        }

        private List<string> GetDistinctGiangViens()
        {
            return _context.KhoaHocs
                .Where(k => !string.IsNullOrEmpty(k.GiangVien))
                .Select(k => k.GiangVien)
                .Distinct()
                .ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
