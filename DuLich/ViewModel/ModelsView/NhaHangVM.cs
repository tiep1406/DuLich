using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ModelsView
{
    public class NhaHangVM
    {
        public int IdNH { get; set; }

        [Required]
        public int ChuDichVu { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public decimal Gia { get; set; }

        [Required]
        public string TenNhaHang { get; set; }

        public IFormFile AnhDaiDien { get; set; }

        [Required]
        public string ChiTietNhaHang { get; set; }

        [Required]
        public string MoTaNhaHang { get; set; }
    }
}