using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Models;
using X.PagedList;
using TrungTamQuanLiDT.Data;
using X.PagedList.Extensions;

namespace TrungTamQuanLiDT.Controllers
{
    public class ThongTinKhoaHocController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ThongTinKhoaHocController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index(string searchString, string giangVien, DateTime? fromDate, DateTime? toDate, int page = 1)
        {
            int pageSize = 3;
            var query = _context.KhoaHocs.AsQueryable();

            // Lọc theo tên khoá học
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

            // Truyền danh sách giảng viên duy nhất về View để chọn lọc
            ViewBag.GiangViens = _context.KhoaHocs
                .Where(k => !string.IsNullOrEmpty(k.GiangVien))
                .Select(k => k.GiangVien)
                .Distinct()
                .ToList();

            ViewBag.SearchString = searchString;
            ViewBag.GiangVienSelected = giangVien;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View(query
                .OrderBy(k => k.ThoiGianKhaiGiang)
                .ToPagedList(page, pageSize));
        }


        public ActionResult Details(int id)
        {
            var khoaHoc = _context.KhoaHocs.FirstOrDefault(k => k.MaKhoaHoc == id);
            return View(khoaHoc);
        }
    }
}
