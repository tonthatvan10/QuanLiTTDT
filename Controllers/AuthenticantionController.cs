using Microsoft.AspNetCore.Mvc;
using TrungTamQuanLiDT.ViewModel;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace TrungTamQuanLiDT.Controllers
{
    public class AuthenticantionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthenticantionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Admin");
                else if (User.IsInRole("HocVien"))
                    return RedirectToAction("Index", "HocVien");
            }
            return View();
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Admin");
                else if (User.IsInRole("HocVien"))
                    return RedirectToAction("Index", "HocVien");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                // ... logic đăng nhập ...
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Lỗi hệ thống: " + ex.Message);
                return View(model);
            }

            var user = _context.HocViens.FirstOrDefault(u => u.TaiKhoan == model.TaiKhoan);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.MatKhau, user.MatKhau))
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View(model);
            }

            // Tạo danh sách claims cho người dùng
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TaiKhoan),
                new Claim(ClaimTypes.Role, user.Role.ToString())  // Lưu role của người dùng
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Đăng nhập và lưu cookie xác thực
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // Điều hướng người dùng tới trang tương ứng với phân quyền
            if (user.Role.ToString() == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.Role.ToString() == "HocVien")
            {
                return RedirectToAction("Index", "HocVien");
            }

            return RedirectToAction("Home");

        }


        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất và xoá cookie xác thực
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["LoginSuccess"] = "Bạn đã đăng xuất thành công.";
            return RedirectToAction("Index", "Authentication");
        }
    }
}
