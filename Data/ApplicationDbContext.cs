using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Models;
using TrungTamQuanLiDT.ViewModel;
using static TrungTamQuanLiDT.Models.UserModel;


namespace TrungTamQuanLiDT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<UserModel> HocViens { get; set; }

        public DbSet<KhoaHocModel> KhoaHocs { get; set; }
        public DbSet<DangKyKhoaHocModel> DangKyHocs { get; set; }

        public DbSet<LoginViewModel> TaiKhoans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KhoaHocModel>()
                .Property(k => k.HocPhi)
                .HasColumnType("decimal(18, 2)")
                .HasPrecision(18, 2);


            modelBuilder.Entity<UserModel>().HasData(
                new UserModel { MaHocVien = 1, HoTen = "Nguyen Van A", TaiKhoan = "nguyenvana", MatKhau = "Aa123456!", Email = "a@gmail.com", SDT = "0901000001", NgaySinh = new DateTime(2000, 1, 1), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 2, HoTen = "Tran Thi B", TaiKhoan = "tranthib", MatKhau = "Bb987654@", Email = "b@gmail.com", SDT = "0901000002", NgaySinh = new DateTime(2000, 2, 2), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 3, HoTen = "Le Van C", TaiKhoan = "levanc", MatKhau = "Cc456789#", Email = "c@gmail.com", SDT = "0901000003", NgaySinh = new DateTime(2000, 3, 3), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 4, HoTen = "Pham Thi D", TaiKhoan = "phamthid", MatKhau = "Dd654321$", Email = "d@gmail.com", SDT = "0901000004", NgaySinh = new DateTime(2000, 4, 4), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 5, HoTen = "Hoang Van E", TaiKhoan = "hoangvane", MatKhau = "Ee112233%", Email = "e@gmail.com", SDT = "0901000005", NgaySinh = new DateTime(2000, 5, 5), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 6, HoTen = "Dang Thi F", TaiKhoan = "dangthif", MatKhau = "Ff445566^", Email = "f@gmail.com", SDT = "0901000006", NgaySinh = new DateTime(2000, 6, 6), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 7, HoTen = "Bui Van G", TaiKhoan = "buivang", MatKhau = "Gg778899&", Email = "g@gmail.com", SDT = "0901000007", NgaySinh = new DateTime(2000, 7, 7), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 8, HoTen = "Do Thi H", TaiKhoan = "dothih", MatKhau = "Hh990011*", Email = "h@gmail.com", SDT = "0901000008", NgaySinh = new DateTime(2000, 8, 8), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 9, HoTen = "Nguyen Van I", TaiKhoan = "nguyenvani", MatKhau = "Ii334455(", Email = "i@gmail.com", SDT = "0901000009", NgaySinh = new DateTime(2000, 9, 9), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 10, HoTen = "Tran Thi J", TaiKhoan = "tranthij", MatKhau = "Jj667788)", Email = "j@gmail.com", SDT = "0901000010", NgaySinh = new DateTime(2000, 10, 10), Role = UserRole.HocVien },
                new UserModel { MaHocVien = 11, HoTen = "Admin One", TaiKhoan = "admin1", MatKhau = "Admin@123", Email = "admin1@domain.com", SDT = "0902000001", NgaySinh = new DateTime(1990, 1, 1), Role = UserRole.Admin },
                new UserModel { MaHocVien = 12, HoTen = "Admin Two", TaiKhoan = "admin2", MatKhau = "Admin#456", Email = "admin2@domain.com", SDT = "0902000002", NgaySinh = new DateTime(1990, 2, 2), Role = UserRole.Admin }

            );

            modelBuilder.Entity<KhoaHocModel>().HasData(
                new KhoaHocModel { MaKhoaHoc = 1, TenKhoaHoc = "Lập trình C# cơ bản", GiangVien = "Nguyễn Văn A", ThoiGianKhaiGiang = new DateTime(2025, 5, 1), HocPhi = 2500000, SoLuongHocVienToiDa = 30 },
                new KhoaHocModel { MaKhoaHoc = 2, TenKhoaHoc = "ASP.NET Core MVC nâng cao", GiangVien = "Trần Thị B", ThoiGianKhaiGiang = new DateTime(2025, 5, 15), HocPhi = 3200000, SoLuongHocVienToiDa = 25 },
                new KhoaHocModel { MaKhoaHoc = 3, TenKhoaHoc = "Cơ sở dữ liệu với SQL Server", GiangVien = "Lê Văn C", ThoiGianKhaiGiang = new DateTime(2025, 6, 1), HocPhi = 2700000, SoLuongHocVienToiDa = 35 },
                new KhoaHocModel { MaKhoaHoc = 4, TenKhoaHoc = "Phát triển Web Fullstack", GiangVien = "Phạm Thị D", ThoiGianKhaiGiang = new DateTime(2025, 6, 10), HocPhi = 5000000, SoLuongHocVienToiDa = 20 },
                new KhoaHocModel { MaKhoaHoc = 5, TenKhoaHoc = "Thiết kế giao diện với HTML/CSS", GiangVien = "Hoàng Văn E", ThoiGianKhaiGiang = new DateTime(2025, 5, 25), HocPhi = 1800000, SoLuongHocVienToiDa = 40 },
                new KhoaHocModel { MaKhoaHoc = 6, TenKhoaHoc = "Lập trình Python cơ bản", GiangVien = "Đặng Thị F", ThoiGianKhaiGiang = new DateTime(2025, 6, 20), HocPhi = 2600000, SoLuongHocVienToiDa = 30 },
                new KhoaHocModel { MaKhoaHoc = 7, TenKhoaHoc = "Kỹ thuật phân tích hệ thống", GiangVien = "Bùi Văn G", ThoiGianKhaiGiang = new DateTime(2025, 7, 1), HocPhi = 2900000, SoLuongHocVienToiDa = 25 }
            );
        }
    }
}
