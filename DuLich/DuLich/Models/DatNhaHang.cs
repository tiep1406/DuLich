using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class DatNhaHang : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        [ForeignKey("NhaHang")]
        public int IdNhaHang { get; set; }

        public DateTime NgayDat { get; set; }
        public DateTime NgayTra { get; set; }
        public NhaHang NhaHang { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}