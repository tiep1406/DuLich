using DuLich.Models;

namespace DuLich.ModelsView
{
    public class NhaHangVM
    {
        public int Id { get; set; }
        public int ChuDichVu { get; set; }
        public string DiaChi { get; set; }
        public int Gia { get; set; }
        public string TenNhaHang { get; set; }
        public string AnhDaiDien { get; set; }
        public string ChiTietNhaHang { get; set; }
        public string MoTaNhaHang { get; set; }
        public int DanhGia { get; set; }
    }
}