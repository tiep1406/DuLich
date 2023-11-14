using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class BinhLuanDiemThamQuan : BaseEntity
    {
        [ForeignKey("DiemThamQuan")]
        public int? IdDiemThamQuan { get; set; }

        [ForeignKey("NguoiDung")]
        public int? IdNguoiDung { get; set; }

        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public double Rating { get; set; }
        public string Reply { get; set; }
        public DiemThamQuan DiemThamQuan { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}