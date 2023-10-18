using DuLich.Models;
using DuLich.Repository.DBContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DuLich.Repository.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public AppDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NguoiDungConfiguration).Assembly);
        }

        public DbSet<Models.DatKhachSan> DatKhachSans { get; set; }
        public DbSet<Models.DatNhaHang> DatNhaHangs { get; set; }
        public DbSet<Models.DatTour> DatTours { get; set; }
        public DbSet<Models.DatVanChuyen> DatVanChuyens { get; set; }
        public DbSet<Models.DiemThamQuan> DiemThamQuans { get; set; }
        public DbSet<Models.DiemThamQuanCT> DiemThamQuanCTs { get; set; }
        public DbSet<Models.KhachSan> KhachSans { get; set; }
        public DbSet<Models.NguoiDung> NguoiDungs { get; set; }
        public DbSet<Models.NhaHang> NhaHangs { get; set; }
        public DbSet<Models.Tour> Tours { get; set; }
        public DbSet<Models.TourCT> TourCTs { get; set; }
        public DbSet<Models.VanChuyen> VanChuyens { get; set; }
    }
}