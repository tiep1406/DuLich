using DuLich.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DuLich.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<KhachSan> KhachSans { get; set; }
        public DbSet<NhaHang> NhaHangs { get; set; }
        public DbSet<VanChuyen> VanChuyens { get; set; }
        public DbSet<DatNhaHang> DatNhaHangs { get; set; }
        public DbSet<DatKhachSan> DatKhachSans { get; set; }
        public DbSet<DatVanChuyen> DatVanChuyens { get; set; }
    }
}
