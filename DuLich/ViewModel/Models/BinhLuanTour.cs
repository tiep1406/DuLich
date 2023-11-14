using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Models
{
    public class BinhLuanTour : BaseEntity
    {
        [ForeignKey("Tour")]
        public int? IdTour { get; set; }

        [ForeignKey("NguoiDung")]
        public int? IdNguoiDung { get; set; }

        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public double Rating { get; set; }
        public string Reply { get; set; }
        public Tour Tour { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}