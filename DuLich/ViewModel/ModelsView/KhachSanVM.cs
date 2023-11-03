using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ModelsView
{
    public class KhachSanVM
    {
        public int IdKS { get; set; }

        [Required]
        public int ChuDichVu { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public int Gia { get; set; }

        [Required]
        public string TenKhachSan { get; set; }

        public IFormFile AnhDaiDien { get; set; }

        [Required]
        public string ChiTietKhachSan { get; set; }

        [Required]
        public string MoTaKhachSan { get; set; }
    }
}