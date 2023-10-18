using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class DatVanChuyen : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        [ForeignKey("VanChuyen")]
        public int IdVanChuyen { get; set; }

        public DateTime NgayDat { get; set; }

        public VanChuyen VanChuyen { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}