using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Models
{
    public class TourCT : BaseEntity
    {
        public int ChuTour { get; set; }

        [ForeignKey("Tour")]
        public int MaTour { get; set; }

        public string LichTrinhNgay { get; set; }
        public string ChiTietLichTrinh { get; set; }
        public string DiaDiem { get; set; }
        public Tour Tour { get; set; }
    }
}