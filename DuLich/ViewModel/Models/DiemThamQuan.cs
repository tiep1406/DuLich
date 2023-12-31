﻿using System.ComponentModel.DataAnnotations.Schema;
using ViewModel.Models;

namespace ViewModel.Models
{
    public class DiemThamQuan : BaseEntity
    {
        [ForeignKey("NguoiDung")]
        public int ChuDichVu { get; set; }

        public NguoiDung NguoiDung { get; set; }
        public string TenDiaDiem { get; set; }
        public decimal Gia { get; set; }
        public string DiaDiem { get; set; }
        public string AnhDaiDien { get; set; }
        public bool TrangThai { get; set; } = true;
        public DiemThamQuanCT DiemThamQuanCT { get; set; }
        public List<BinhLuanDiemThamQuan> BinhLuanDiemThamQuans { get; set; }
    }
}