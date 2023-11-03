using ViewModel.Models;

namespace ViewModel.Models
{
    public class DiemThamQuan : BaseEntity
    {
        public int ChuDichVu { get; set; }
        public string TenDiaDiem { get; set; }
        public decimal Gia { get; set; }
        public string DiaDiem { get; set; }
        public string AnhDaiDien { get; set; }
        public DiemThamQuanCT DiemThamQuanCT { get; set; }
    }
}