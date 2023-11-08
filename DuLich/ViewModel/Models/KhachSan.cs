using System.ComponentModel.DataAnnotations.Schema;
using ViewModel.Models;

namespace ViewModel.Models
{
    public class KhachSan : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int ChuDichVu { get; set; }

        public NguoiDung NguoiDung { get; set; }
        public string DiaChi { get; set; }
        public int Gia { get; set; }
        public string TenKhachSan { get; set; }
        public string AnhDaiDien { get; set; }
        public string ChiTietKhachSan { get; set; }
        public string MoTaKhachSan { get; set; }
        public int DanhGia { get; set; }
        public bool TrangThai { get; set; } = true;
        public List<DatKhachSan> DatKhachSans { get; set; }
        public List<BinhLuanKhachSan> BinhLuanKhachSans { get; set; }
    }
}