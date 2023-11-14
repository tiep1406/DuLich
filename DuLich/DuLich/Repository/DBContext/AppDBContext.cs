using DuLich.Repository.DBContext.Configurations;
using Microsoft.EntityFrameworkCore;
using ViewModel.Models;

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

        public DbSet<DatKhachSan> DatKhachSans { get; set; }
        public DbSet<DatNhaHang> DatNhaHangs { get; set; }
        public DbSet<ViewModel.Models.DatTour> DatTours { get; set; }
        public DbSet<ViewModel.Models.DatVanChuyen> DatVanChuyens { get; set; }
        public DbSet<ViewModel.Models.DiemThamQuan> DiemThamQuans { get; set; }
        public DbSet<ViewModel.Models.DiemThamQuanCT> DiemThamQuanCTs { get; set; }
        public DbSet<ViewModel.Models.KhachSan> KhachSans { get; set; }
        public DbSet<ViewModel.Models.NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhaHang> NhaHangs { get; set; }
        public DbSet<ViewModel.Models.Tour> Tours { get; set; }
        public DbSet<TourCT> TourCTs { get; set; }
        public DbSet<VanChuyen> VanChuyens { get; set; }
        public DbSet<ViewModel.Models.BinhLuanTour> BinhLuanTours { get; set; }
        public DbSet<ViewModel.Models.BinhLuanNhaHang> BinhLuanNhaHangs { get; set; }
        public DbSet<ViewModel.Models.BinhLuanKhachSan> BinhLuanKhachSans { get; set; }
        public DbSet<ViewModel.Models.BinhLuanDiemThamQuan> BinhLuanDiemThamQuans { get; set; }
        public DbSet<ViewModel.Models.BinhLuanVanChuyen> BinhLuanVanChuyens { get; set; }
    }
}