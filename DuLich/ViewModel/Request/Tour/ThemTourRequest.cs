using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.Tour
{
    public class ThemTourRequest
    {
        [Required]
        public int ChuTour { get; set; }

        [Required]
        public string TenTour { get; set; }

        [Required]
        public int SoNgay { get; set; }

        [Required]
        public string ChiTietTour { get; set; }

        [Required]
        public decimal KhuyenMaiTour { get; set; }

        [Required]
        public string MoTaTour { get; set; }

        public IFormFile HinhAnhTour { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        public decimal GiaTour { get; set; }

        [Required]
        public string LichTrinhNgay { get; set; }

        [Required]
        public string ChiTietLichTrinh { get; set; }

        [Required]
        public string DiaDiem { get; set; }
    }
}