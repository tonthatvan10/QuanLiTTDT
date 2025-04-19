using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrungTamQuanLiDT.Migrations
{
    /// <inheritdoc />
    public partial class CreateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "HocViens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "HocViens",
                columns: new[] { "MaHocVien", "Email", "HoTen", "MatKhau", "NgaySinh", "Role", "SDT", "TaiKhoan" },
                values: new object[,]
                {
                    { 1, "a@gmail.com", "Nguyen Van A", "123456", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000001", "nguyenvana" },
                    { 2, "b@gmail.com", "Tran Thi B", "123456", new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000002", "tranthib" },
                    { 3, "c@gmail.com", "Le Van C", "123456", new DateTime(2000, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000003", "levanc" },
                    { 4, "d@gmail.com", "Pham Thi D", "123456", new DateTime(2000, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000004", "phamthid" },
                    { 5, "e@gmail.com", "Hoang Van E", "123456", new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000005", "hoangvane" },
                    { 6, "f@gmail.com", "Dang Thi F", "123456", new DateTime(2000, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000006", "dangthif" },
                    { 7, "g@gmail.com", "Bui Van G", "123456", new DateTime(2000, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000007", "buivang" },
                    { 8, "h@gmail.com", "Do Thi H", "123456", new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000008", "dothih" },
                    { 9, "i@gmail.com", "Nguyen Van I", "123456", new DateTime(2000, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000009", "nguyenvani" },
                    { 10, "j@gmail.com", "Tran Thi J", "123456", new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "0901000010", "tranthij" },
                    { 11, "admin1@domain.com", "Admin One", "admin123", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "0902000001", "admin1" },
                    { 12, "admin2@domain.com", "Admin Two", "admin123", new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "0902000002", "admin2" }
                });

            migrationBuilder.InsertData(
                table: "KhoaHocs",
                columns: new[] { "MaKhoaHoc", "GiangVien", "HocPhi", "SoLuongHocVienToiDa", "TenKhoaHoc", "ThoiGianKhaiGiang" },
                values: new object[,]
                {
                    { 1, "Nguyễn Văn A", 2500000m, 30, "Lập trình C# cơ bản", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Trần Thị B", 3200000m, 25, "ASP.NET Core MVC nâng cao", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Lê Văn C", 2700000m, 35, "Cơ sở dữ liệu với SQL Server", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Phạm Thị D", 5000000m, 20, "Phát triển Web Fullstack", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Hoàng Văn E", 1800000m, 40, "Thiết kế giao diện với HTML/CSS", new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Đặng Thị F", 2600000m, 30, "Lập trình Python cơ bản", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Bùi Văn G", 2900000m, 25, "Kỹ thuật phân tích hệ thống", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "HocViens");
        }
    }
}
