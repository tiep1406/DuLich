using Microsoft.AspNetCore.Http;

namespace ViewModel.ModelsView
{
    public class KhachSanVM
    {
        public int IdKS { get; set; }
        public int ChuDichVu { get; set; }
        public string DiaChi { get; set; }
        public int Gia { get; set; }
        public string TenKhachSan { get; set; }
        public IFormFile AnhDaiDien { get; set; }
        public string ChiTietKhachSan { get; set; }
        public string MoTaKhachSan { get; set; }
        public int DanhGia { get; set; }
    }
}