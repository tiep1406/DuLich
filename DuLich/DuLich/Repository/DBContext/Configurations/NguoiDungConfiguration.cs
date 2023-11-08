using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DuLich.Repository.DBContext.Configurations
{
    public class NguoiDungConfiguration : IEntityTypeConfiguration<ViewModel.Models.NguoiDung>
    {
        public void Configure(EntityTypeBuilder<ViewModel.Models.NguoiDung> builder)
        {
            builder.HasData(new ViewModel.Models.NguoiDung()
            {
                Id = 1,
                CCCD = "123456789",
                Email = "admin@admin.com",
                GioiTinh = 1,
                Sdt = "0123456789",
                NoiO = "Ha Noi",
                PhanQuyen = 0,
                TrangThai = 1,
                MatKhau = "i2yBMU+FxDo=", // 123456
                HoTen = "Admin",
                AnhDaiDien = "default-avatar.jpg"
            });
        }
    }
}