﻿using DuLich.Models;

namespace DuLich.ModelsView
{
    public class VanChuyenVM
    {
        public int Id { get; set; }
        public int ChuDichVu { get; set; }
        public string DiaChiDung { get; set; }
        public int Gia { get; set; }
        public DateTime ThoiGian { get; set; }
        public string AnhDaiDien { get; set; }
        public string ChiTietDiemDung { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TaiXe { get; set; }

    }
}
