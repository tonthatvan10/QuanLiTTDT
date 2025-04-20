using Microsoft.AspNetCore.Mvc;
using TrungTamQuanLiDT.ViewModel;
using TrungTamQuanLiDT.Data;
using TrungTamQuanLiDT.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using static TrungTamQuanLiDT.Models.UserModel;

namespace TrungTamQuanLiDT.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController(ApplicationDbContext context)
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

        //GET: Authentication/Register
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Admin");
                else if (User.IsInRole("User"))
                    return RedirectToAction("Index", "User");
            }

            return View();
        }

        //POST: Authentication/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra tài khoản tồn tại
            if (_context.HocViens.Any(u => u.TaiKhoan == model.TaiKhoan))
            {
                ModelState.AddModelError("TaiKhoan", "Tài khoản đã được sử dụng");
                return View(model);
            }

            // Tạo user mới
            var newUser = new UserModel
            {
                TaiKhoan = model.TaiKhoan,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau),
                Role = UserModel.UserRole.HocVien,
            };

            try
            {
                _context.HocViens.Add(newUser);
                await _context.SaveChangesAsync();

                // Thêm thông báo thành công và chuyển hướng về login
                TempData["RegisterSuccess"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login", "Authentication"); // Thay "Login" bằng tên action đăng nhập của bạn
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                return View(model);
            }
        }


        // GET: Authenticantion/Login
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index", "Admin");
                else if (User.IsInRole("HocVien"))
                    return RedirectToAction("Index", "HocVien");
            }
            return View("~/Views/Authentication/Login.cshtml");
        }

        // POST: Authentication/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
            return user.Role switch
            {
                UserRole.Admin => RedirectToAction("Index", "Admin"),
                UserRole.HocVien => RedirectToAction("Index", "HocVien"),
                _ => RedirectToAction("Index", "Home")
            };
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
