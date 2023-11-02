using Microsoft.AspNetCore.Http;

namespace ViewModel.ModelsView
{
    public class NhaHangVM
    {
        public int IdNH { get; set; }
        public int ChuDichVu { get; set; }
        public string DiaChi { get; set; }
        public int Gia { get; set; }
        public string TenNhaHang { get; set; }
        public IFormFile AnhDaiDien { get; set; }
        public string ChiTietNhaHang { get; set; }
        public string MoTaNhaHang { get; set; }
        public int DanhGia { get; set; }
    }
}