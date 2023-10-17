namespace DuLich.Models
{
    public class TourCT:BaseEntity
    {
        public int IdTour { get; set; }
        public int MaTour { get; set; }
        public string TenTour { get; set; }
        public string LichTrinhNgay { get; set; }
        public string ChiTietLichTrinh { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
