using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungTamQuanLiDT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPasswordRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 1,
                column: "MatKhau",
                value: "Aa123456!");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 2,
                column: "MatKhau",
                value: "Bb987654@");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 3,
                column: "MatKhau",
                value: "Cc456789#");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 4,
                column: "MatKhau",
                value: "Dd654321$");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 5,
                column: "MatKhau",
                value: "Ee112233%");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 6,
                column: "MatKhau",
                value: "Ff445566^");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 7,
                column: "MatKhau",
                value: "Gg778899&");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 8,
                column: "MatKhau",
                value: "Hh990011*");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 9,
                column: "MatKhau",
                value: "Ii334455(");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 10,
                column: "MatKhau",
                value: "Jj667788)");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 11,
                column: "MatKhau",
                value: "Admin@123");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 12,
                column: "MatKhau",
                value: "Admin#456");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 1,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 2,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 3,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 4,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 5,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 6,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 7,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 8,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 9,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 10,
                column: "MatKhau",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 11,
                column: "MatKhau",
                value: "admin123");

            migrationBuilder.UpdateData(
                table: "HocViens",
                keyColumn: "MaHocVien",
                keyValue: 12,
                column: "MatKhau",
                value: "admin123");
        }
    }
}
