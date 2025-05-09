﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrungTamQuanLiDT.Data;

#nullable disable

namespace TrungTamQuanLiDT.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250419141003_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TrungTamQuanLiDT.Models.DangKyKhoaHocModel", b =>
                {
                    b.Property<int>("MaDangKy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDangKy"));

                    b.Property<int>("MaHocVien")
                        .HasColumnType("int");

                    b.Property<int>("MaKhoaHoc")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDangKy")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaDangKy");

                    b.HasIndex("MaHocVien");

                    b.HasIndex("MaKhoaHoc");

                    b.ToTable("DangKyHocs");
                });

            modelBuilder.Entity("TrungTamQuanLiDT.Models.KhoaHocModel", b =>
                {
                    b.Property<int>("MaKhoaHoc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhoaHoc"));

                    b.Property<string>("GiangVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("HocPhi")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("SoLuongHocVienToiDa")
                        .HasColumnType("int");

                    b.Property<string>("TenKhoaHoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ThoiGianKhaiGiang")
                        .HasColumnType("datetime2");

                    b.HasKey("MaKhoaHoc");

                    b.ToTable("KhoaHocs");
                });

            modelBuilder.Entity("TrungTamQuanLiDT.Models.UserModel", b =>
                {
                    b.Property<int>("MaHocVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHocVien"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaiKhoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHocVien");

                    b.ToTable("HocViens");
                });

            modelBuilder.Entity("TrungTamQuanLiDT.Models.DangKyKhoaHocModel", b =>
                {
                    b.HasOne("TrungTamQuanLiDT.Models.UserModel", "HocVien")
                        .WithMany("DangKyMonHocs")
                        .HasForeignKey("MaHocVien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrungTamQuanLiDT.Models.KhoaHocModel", "KhoaHoc")
                        .WithMany("DangKyHocs")
                        .HasForeignKey("MaKhoaHoc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HocVien");

                    b.Navigation("KhoaHoc");
                });

            modelBuilder.Entity("TrungTamQuanLiDT.Models.KhoaHocModel", b =>
                {
                    b.Navigation("DangKyHocs");
                });

            modelBuilder.Entity("TrungTamQuanLiDT.Models.UserModel", b =>
                {
                    b.Navigation("DangKyMonHocs");
                });
#pragma warning restore 612, 618
        }
    }
}
