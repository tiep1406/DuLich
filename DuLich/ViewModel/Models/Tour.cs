using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Models
{
    public class Tour : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int ChuTour { get; set; }

        public NguoiDung NguoiDung { get; set; }
        public string TenTour { get; set; }
        public int SoNgay { get; set; }
        public int LuotDanhGia { get; set; }
        public string ChiTietTour { get; set; }
        public decimal KhuyenMaiTour { get; set; }
        public string MoTaTour { get; set; }
        public string HinhAnhTour { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal GiaTour { get; set; }
        public List<DatTour> DatTours { get; set; }
        public TourCT TourCT { get; set; }
    }
}