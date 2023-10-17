namespace DuLich.Models
{
    public class NhaHang : BaseEntity
    {
        public int ChuDichVu { get; set; }
        public string DiaChi { get; set; }
        public int Gia { get; set; }
        public string TenKhachSan { get; set; }
        public string AnhDaiDien { get; set; }
        public string ChiTietKhachSan { get; set; }
        public string MoTaKhachSan { get; set; }
        public int DanhGia { get; set; }

    }
}
