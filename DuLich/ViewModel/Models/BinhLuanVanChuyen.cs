using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Models
{
    public class BinhLuanVanChuyen : BaseEntity
    {
        [ForeignKey("VanChuyen")]
        public int IdVanChuyen { get; set; }

        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        public int ChuDichVu { get; set; }

        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public VanChuyen VanChuyen { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}