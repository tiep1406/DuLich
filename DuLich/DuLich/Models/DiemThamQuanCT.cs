using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class DiemThamQuanCT : BaseEntity
    {
        [ForeignKey("DiemThamQuan")]
        public int MaDichVu { get; set; }

        public string AnhChiTiet { get; set; }
        public string MoTaDichVu { get; set; }
        public string DanhGia { get; set; }
        public DiemThamQuan DiemThamQuan { get; set; }
    }
}