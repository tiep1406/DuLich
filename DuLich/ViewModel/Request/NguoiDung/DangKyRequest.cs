using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.NguoiDung
{
    public class DangKyRequest
    {
        [Required]
        public string HoTen { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Sdt { get; set; }

        [Required]
        public string MatKhau { get; set; }

        [Required]
        public string NoiO { get; set; }

        [Required]
        public int PhanQuyen { get; set; }

        [Required]
        public int GioiTinh { get; set; }

        [Required]
        public string CCCD { get; set; }

        public List<int> DichVus { get; set; } = new List<int>();
    }
}