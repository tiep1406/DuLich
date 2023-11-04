namespace ViewModel.Models
{
    public class VanChuyen : BaseEntity
    {
        public int ChuDichVu { get; set; }
        public string DiaChiDung { get; set; }
        public string DiaChiDi { get; set; }
        public decimal Gia { get; set; }
        public string AnhDaiDien { get; set; }
        public string ChiTietDiemDung { get; set; }
        public string ChiTietDiemDi { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TaiXe { get; set; }
        public List<DatVanChuyen> DatVanChuyens { get; set; }
        public List<BinhLuanVanChuyen> BinhLuanVanChuyens { get; set; }
    }
}