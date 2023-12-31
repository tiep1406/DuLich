﻿using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.NguoiDung
{
    public class DangNhapRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}