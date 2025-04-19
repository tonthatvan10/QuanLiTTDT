using Microsoft.EntityFrameworkCore;
using TrungTamQuanLiDT.Models;


namespace TrungTamQuanLiDT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<UserModel> HocViens { get; set; }
        public DbSet<KhoaHocModel> KhoaHocs { get; set; }
        public DbSet<DangKyKhoaHocModel> DangKyHocs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KhoaHocModel>()
                .Property(k => k.HocPhi)
                .HasColumnType("decimal(18, 2)")
                .HasPrecision(18, 2);
        }
    }
}
