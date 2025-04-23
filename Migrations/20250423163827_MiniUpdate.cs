using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrungTamQuanLiDT.Migrations
{
    /// <inheritdoc />
    public partial class MiniUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocs_HocViens_MaHocVien",
                table: "DangKyHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocs_KhoaHocs_MaKhoaHoc",
                table: "DangKyHocs");

            migrationBuilder.AlterColumn<string>(
                name: "TaiKhoan",
                table: "HocViens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "DangKyHocs",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_HocViens_TaiKhoan",
                table: "HocViens",
                column: "TaiKhoan",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocs_HocViens_MaHocVien",
                table: "DangKyHocs",
                column: "MaHocVien",
                principalTable: "HocViens",
                principalColumn: "MaHocVien",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocs_KhoaHocs_MaKhoaHoc",
                table: "DangKyHocs",
                column: "MaKhoaHoc",
                principalTable: "KhoaHocs",
                principalColumn: "MaKhoaHoc",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocs_HocViens_MaHocVien",
                table: "DangKyHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKyHocs_KhoaHocs_MaKhoaHoc",
                table: "DangKyHocs");

            migrationBuilder.DropIndex(
                name: "IX_HocViens_TaiKhoan",
                table: "HocViens");

            migrationBuilder.AlterColumn<string>(
                name: "TaiKhoan",
                table: "HocViens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "DangKyHocs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocs_HocViens_MaHocVien",
                table: "DangKyHocs",
                column: "MaHocVien",
                principalTable: "HocViens",
                principalColumn: "MaHocVien",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHocs_KhoaHocs_MaKhoaHoc",
                table: "DangKyHocs",
                column: "MaKhoaHoc",
                principalTable: "KhoaHocs",
                principalColumn: "MaKhoaHoc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
