using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.DiemThamQuan
{
    public class ThemDiemThamQuanRequest
    {
        [Required]
        public int ChuDichVu { get; set; }

        [Required]
        public string TenDiaDiem { get; set; }

        [Required]
        public decimal Gia { get; set; }

        [Required]
        public string DiaDiem { get; set; }

        public IFormFile AnhDaiDien { get; set; }

        public IFormFile AnhChiTiet { get; set; }

        [Required]
        public string MoTaDichVu { get; set; }
    }
}