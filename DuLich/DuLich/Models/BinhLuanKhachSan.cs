﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DuLich.Models
{
    public class BinhLuanKhachSan : BaseEntity
    {
        [ForeignKey("KhachSan")]
        public int IdKhachSan { get; set; }

        public KhachSan KhachSan { get; set; }

        [ForeignKey("NguoiDung")]
        public int IdNguoiDung { get; set; }

        public NguoiDung NguoiDung { get; set; }

        public int ChuDichVu { get; set; }

        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}