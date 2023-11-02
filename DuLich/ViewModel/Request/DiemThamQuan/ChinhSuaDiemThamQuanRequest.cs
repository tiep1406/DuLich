using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.DiemThamQuan
{
    public class ChinhSuaDiemThamQuanRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ChuDichVu { get; set; }

        [Required]
        public string TenDiaDiem { get; set; }

        [Required]
        public int Gia { get; set; }

        [Required]
        public string DiaDiem { get; set; }

        public IFormFile AnhDaiDien { get; set; }
        public IFormFile AnhChiTiet { get; set; }

        [Required]
        public string MoTaDichVu { get; set; }
    }
}