namespace DuLich.Models
{
    public class NguoiDung:BaseEntity
    {
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string MatKhau { get; set; }
        public int TrangThai { get; set; }
        public int PhanQuyen { get; set; }
        public string NoiO { get; set; }
        public int GioiTinh { get; set; }
        public string CCCD { get; set; }
        public List<DatKhachSan> DatKhachSans { get; set; }
        public List<DatNhaHang> DatNhaHangs { get; set; }
        public List<DatTour> DatTours { get; set; }
        public List<DatVanChuyen> DatVanChuyens { get; set; }
    }
}
