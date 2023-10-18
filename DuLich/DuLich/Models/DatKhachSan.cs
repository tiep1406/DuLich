namespace DuLich.Models
{
    public class DatKhachSan
    {
        public int IdNguoiDung { get; set; }
        public int IdKhachSan { get; set;}
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual KhachSan KhachSan { get; set; }
    }
}
