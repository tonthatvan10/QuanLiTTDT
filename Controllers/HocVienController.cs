using Microsoft.AspNetCore.Mvc;
using TrungTamQuanLiDT.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace TrungTamQuanLiDT.Controllers
{
    [Authorize(Roles = "HocVien")]
    public class HocVienController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
