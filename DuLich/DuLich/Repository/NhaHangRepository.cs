using DuLich.Models;
using DuLich.ModelsView;
using DuLich.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoCrud.Responsitory
{
    public class NhaHangRepository : INhaHangRepositoty
    {
        private AppDBContext _DBContext;

        public NhaHangRepository(AppDBContext dBContext)
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
                TenNhaHang = nhaHang.TenNhaHang,
                AnhDaiDien = nhaHang.AnhDaiDien,
                ChiTietNhaHang = nhaHang.ChiTietNhaHang,
                MoTaNhaHang = nhaHang.MoTaNhaHang,
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
            _nhahang.TenNhaHang = nhaHang.TenNhaHang;
            _nhahang.AnhDaiDien = nhaHang.AnhDaiDien;
            _nhahang.ChiTietNhaHang = nhaHang.ChiTietNhaHang;
            _nhahang.MoTaNhaHang = nhaHang.MoTaNhaHang;
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
                NgayDat = datNhaHang.NgayDat,
                NgayTra = datNhaHang.NgayTra,
            };
            await _DBContext.DatNhaHangs.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
            return dat;
        }
    }
}