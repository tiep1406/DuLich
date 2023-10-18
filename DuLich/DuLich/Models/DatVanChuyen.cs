namespace DuLich.Models
{
    public class DatVanChuyen
    {
        public int IdNguoiDung { get; set; }
        public int IdVanChuyen { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual VanChuyen VanChuyen { get; set; }
    }
}
