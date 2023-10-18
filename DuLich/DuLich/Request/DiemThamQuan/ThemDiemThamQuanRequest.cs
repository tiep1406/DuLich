using System.ComponentModel.DataAnnotations;

namespace DuLich.Request.DiemThamQuan
{
    public class ThemDiemThamQuanRequest
    {
        [Required]
        public int ChuDichVu { get; set; }

        [Required]
        public string TenDiaDiem { get; set; }

        [Required]
        public int Gia { get; set; }

        [Required]
        public string DiaDiem { get; set; }

        [Required]
        public IFormFile AnhDaiDien { get; set; }

        [Required]
        public IFormFile AnhChiTiet { get; set; }

        [Required]
        public string MoTaDichVu { get; set; }
    }
}