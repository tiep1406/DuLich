using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class DatTour : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        [ForeignKey("Tour")]
        public int IdTour { get; set; }

        public DateTime NgayDat { get; set; }

        public Tour Tour { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}