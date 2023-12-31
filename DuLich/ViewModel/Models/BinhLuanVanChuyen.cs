﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Models
{
    public class BinhLuanVanChuyen : BaseEntity
    {
        [ForeignKey("VanChuyen")]
        public int? IdVanChuyen { get; set; }

        [ForeignKey("NguoiDung")]
        public int? IdNguoiDung { get; set; }

        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public double Rating { get; set; }
        public string Reply { get; set; }
        public VanChuyen VanChuyen { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}