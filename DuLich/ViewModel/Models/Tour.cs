namespace ViewModel.Models
{
    public class Tour : BaseEntity
    {
        public int ChuTour { get; set; }
        public string TenTour { get; set; }
        public int SoNgay { get; set; }
        public int LuotDanhGia { get; set; }
        public string ChiTietTour { get; set; }
        public int KhuyenMaiTour { get; set; }
        public string MoTaTour { get; set; }
        public string HinhAnhTour { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string GiaTour { get; set; }
        public List<DatTour> DatTours { get; set; }
        public TourCT TourCT { get; set; }
    }
}