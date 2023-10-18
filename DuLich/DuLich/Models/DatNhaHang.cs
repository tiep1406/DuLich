namespace DuLich.Models
{
    public class DatNhaHang
    {
        public int IdNguoiDung { get; set; }
        public int IdNhaHang { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayTra { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NhaHang NhaHang { get; set; }
    }
}
