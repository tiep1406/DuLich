namespace DuLich.Models
{
    public class DiemThamQuan:BaseEntity
    {
        public int ChuDichVu { get; set; }
        public string TenDiaDiem { get; set; }
        public int Gia { get; set; }
        public string DiaDiem { get; set; }
        public string AnhDaiDien { get; set; }
        public List<DiemThamQuanCT> DiemThamQuanCTs { get; set; }
    }
}
