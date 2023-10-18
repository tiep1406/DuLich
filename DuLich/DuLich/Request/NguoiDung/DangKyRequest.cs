using System.ComponentModel.DataAnnotations;

namespace DuLich.Request.NguoiDung
{
    public class DangKyRequest
    {
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
        public int GioiTinh { get; set; }

        [Required]
        public string CCCD { get; set; }
    }
}