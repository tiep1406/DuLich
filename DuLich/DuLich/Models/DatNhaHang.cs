namespace DuLich.Models
{
    public class DatNhaHang
    {
        public int IdNguoiDung { get; set; }
        public int IdNhaHang { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual NhaHang NhaHang { get; set; }
    }
}
