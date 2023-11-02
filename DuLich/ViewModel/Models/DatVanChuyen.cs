using System.ComponentModel.DataAnnotations.Schema;
using ViewModel.Models;

namespace ViewModel.Models
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