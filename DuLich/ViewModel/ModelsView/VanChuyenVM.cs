using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ModelsView
{
    public class VanChuyenVM
    {
        public int IdVC { get; set; }

        [Required]
        public int ChuDichVu { get; set; }

        [Required]
        public string DiaChiDung { get; set; }

        [Required]
        public string DiaChiDi { get; set; }

        [Required]
        public decimal Gia { get; set; }

        public IFormFile AnhDaiDien { get; set; }

        [Required]
        public string ChiTietDiemDung { get; set; }

        [Required]
        public string ChiTietDiemDi { get; set; }

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }

        [Required]
        public string TaiXe { get; set; }
    }
}