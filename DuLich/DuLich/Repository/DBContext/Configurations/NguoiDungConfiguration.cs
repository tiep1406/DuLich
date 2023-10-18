using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DuLich.Repository.DBContext.Configurations
{
    public class NguoiDungConfiguration : IEntityTypeConfiguration<Models.NguoiDung>
    {
        public void Configure(EntityTypeBuilder<Models.NguoiDung> builder)
        {
            builder.HasData(new Models.NguoiDung()
            {
                Id = 1,
                CCCD = "123456789",
                Email = "admin@admin.com",
                GioiTinh = 1,
                Sdt = "0123456789",
                NoiO = "Ha Noi",
                PhanQuyen = 0,
                TrangThai = 1,
                MatKhau = "admin"
            });
        }
    }
}