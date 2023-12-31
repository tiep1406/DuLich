﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Request.NguoiDung
{
    public class ChinhSuaNguoiDungRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        public string Sdt { get; set; }

        public string MatKhau { get; set; }

        [Required]
        public int TrangThai { get; set; }

        [Required]
        public int PhanQuyen { get; set; }

        [Required]
        public string NoiO { get; set; }

        [Required]
        public int GioiTinh { get; set; }

        [Required]
        public string CCCD { get; set; }

        public List<int> DichVus { get; set; } = new List<int>();
        public string DanhSachDichVu { get; set; }
        public IFormFile AnhDaiDien { get; set; }
    }
}