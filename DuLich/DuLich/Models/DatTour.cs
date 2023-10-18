namespace DuLich.Models
{
    public class DatTour
    {
        public int IdNguoiDung { get; set; }
        public int IdTour { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
