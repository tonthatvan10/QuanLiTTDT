using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungTamQuanLiDT.Migrations
{
    /// <inheritdoc />
    public partial class AddThoiGianKetThucColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianKetThuc",
                table: "KhoaHocs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 1,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 2,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 3,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 4,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 5,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 6,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "KhoaHocs",
                keyColumn: "MaKhoaHoc",
                keyValue: 7,
                column: "ThoiGianKetThuc",
                value: new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiGianKetThuc",
                table: "KhoaHocs");
        }
    }
}
