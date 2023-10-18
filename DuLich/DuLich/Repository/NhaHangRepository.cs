using DuLich.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.OleDb;
using System.Data.SqlClient;
using DuLich.Data;
using DuLich.ModelsView;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoCrud.Responsitory
{
    public class NhaHangRepository : INhaHangRepositoty
    {
        private ApplicationDBContext _DBContext;
        public NhaHangRepository(ApplicationDBContext dBContext)
        {
            _DBContext = dBContext;
        }
        public async Task<List<NhaHang>> GetAll()
        {
            var ds = await _DBContext.NhaHangs.Select(t => t).ToListAsync();
            return ds;
        }
        public async Task<NhaHang> GetNhaHang(int id)
        {
            var ds = await _DBContext.NhaHangs.FindAsync(id);
            return ds;
        }
        public async Task<NhaHang> Add(NhaHangVM nhaHang)
        {
            NhaHang user1 = new NhaHang
            {
                ChuDichVu = nhaHang.ChuDichVu,
                DiaChi = nhaHang.DiaChi,
                Gia = nhaHang.Gia,
                TenKhachSan = nhaHang.TenNhaHang,
                AnhDaiDien = nhaHang.AnhDaiDien,
                ChiTietKhachSan = nhaHang.ChiTietNhaHang,
                MoTaKhachSan = nhaHang.MoTaKhachSan,
                DanhGia = nhaHang.DanhGia
            };
            await _DBContext.NhaHangs.AddAsync(user1);
            await _DBContext.SaveChangesAsync();
            return user1;
        }

        public void Delete(int id)
        {
            var nhaHang = _DBContext.NhaHangs.SingleOrDefault(lo => lo.Id == id);
            if (nhaHang != null)
            {
                _DBContext.Remove(nhaHang);
                _DBContext.SaveChanges();
            }
        }


        public void Update(NhaHangVM nhaHang)
        {
            var _nhahang = _DBContext.NhaHangs.SingleOrDefault(lo => lo.Id == nhaHang.Id);
            _nhahang.ChuDichVu = nhaHang.ChuDichVu;
            _nhahang.DiaChi = nhaHang.DiaChi;
            _nhahang.Gia = nhaHang.Gia;
            _nhahang.TenKhachSan = nhaHang.TenNhaHang;
            _nhahang.AnhDaiDien = nhaHang.AnhDaiDien;
            _nhahang.ChiTietKhachSan = nhaHang.ChiTietNhaHang;
            _nhahang.MoTaKhachSan = nhaHang.MoTaKhachSan;
            _nhahang.DanhGia = nhaHang.DanhGia;
            _DBContext.NhaHangs.Update(_nhahang);
            _DBContext.SaveChanges();
        }
        public async Task<DatNhaHang> DatNhaHang(DatNhaHangVM datNhaHang)
        {
            DatNhaHang dat = new DatNhaHang
            {
                IdNguoiDung = datNhaHang.IdNguoiDung,
                IdNhaHang = datNhaHang.IdNhaHang,
            };
            await _DBContext.DatNhaHangs.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
            return dat;
        }
    }
}
