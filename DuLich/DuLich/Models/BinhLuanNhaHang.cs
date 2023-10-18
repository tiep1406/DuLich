﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class BinhLuanNhaHang : BaseEntity
    {
        [ForeignKey("NhaHang")]
        public int IdNhaHang { get; set; }

        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        public int ChuDichVu { get; set; }

        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public NhaHang NhaHang { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}