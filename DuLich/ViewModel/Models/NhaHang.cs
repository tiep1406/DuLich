using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Models
{
    public class NhaHang : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int ChuDichVu { get; set; }

        public NguoiDung NguoiDung { get; set; }
        public string DiaChi { get; set; }
        public decimal Gia { get; set; }
        public string TenNhaHang { get; set; }
        public string AnhDaiDien { get; set; }
        public string ChiTietNhaHang { get; set; }
        public string MoTaNhaHang { get; set; }
        public int DanhGia { get; set; }
        public bool TrangThai { get; set; } = true;
        public List<DatNhaHang> DatNhaHangs { get; set; }
        public List<BinhLuanNhaHang> BinhLuanNhaHangs { get; set; }
    }
}