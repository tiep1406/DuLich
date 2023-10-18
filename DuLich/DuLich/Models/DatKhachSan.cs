using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class DatKhachSan : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        [ForeignKey("KhachSan")]
        public int IdKhachSan { get; set; }

        public DateTime NgayDat { get; set; }
        public DateTime NgayTra { get; set; }

        public KhachSan KhachSan { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}