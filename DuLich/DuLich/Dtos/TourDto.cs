namespace DuLich.Dtos
{
    public class TourDto
    {
        public int Id { get; set; }
        public int ChuTour { get; set; }
        public string TenTour { get; set; }
        public int SoNgay { get; set; }
        public int LuotDanhGia { get; set; }
        public string ChiTietTour { get; set; }
        public int KhuyenMaiTour { get; set; }
        public string MoTaTour { get; set; }
        public string HinhAnhTour { get; set; }
        public string LichTrinhNgay { get; set; }
        public string ChiTietLichTrinh { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string GiaTour { get; set; }

        public DateTime? NgayDat { get; set; }
    }
}