using DuLich.Models;
using DuLich.ModelsView;
using DuLich.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoCrud.Responsitory
{
    public class VanChuyenRepository : IVanChuyenRepositoty
    {
        private AppDBContext _DBContext;

        public VanChuyenRepository(AppDBContext dBContext)
        {
            _DBContext = dBContext;
        }

        public async Task<List<VanChuyen>> GetAll()
        {
            var ds = await _DBContext.VanChuyens.Select(t => t).ToListAsync();
            return ds;
        }

        public async Task<VanChuyen> GetVanChuyen(int id)
        {
            var ds = await _DBContext.VanChuyens.FindAsync(id);
            return ds;
        }

        public async Task<VanChuyen> Add(VanChuyenVM VanChuyen)
        {
            VanChuyen user1 = new VanChuyen
            {
                ChuDichVu = VanChuyen.ChuDichVu,
                DiaChiDung = VanChuyen.DiaChiDung,
                Gia = VanChuyen.Gia,
                ThoiGian = VanChuyen.ThoiGian,
                AnhDaiDien = VanChuyen.AnhDaiDien,
                ChiTietDiemDung = VanChuyen.ChiTietDiemDung,
                ThoiGianBatDau = VanChuyen.ThoiGianBatDau,
                ThoiGianKetThuc = VanChuyen.ThoiGianKetThuc,
                TaiXe = VanChuyen.TaiXe
            };
            await _DBContext.VanChuyens.AddAsync(user1);
            await _DBContext.SaveChangesAsync();
            return user1;
        }

        public void Delete(int id)
        {
            var VanChuyen = _DBContext.VanChuyens.SingleOrDefault(lo => lo.Id == id);
            if (VanChuyen != null)
            {
                _DBContext.Remove(VanChuyen);
                _DBContext.SaveChanges();
            }
        }

        public void Update(VanChuyenVM VanChuyen)
        {
            var _VanChuyen = _DBContext.VanChuyens.SingleOrDefault(lo => lo.Id == VanChuyen.Id);
            _VanChuyen.ChuDichVu = VanChuyen.ChuDichVu;
            _VanChuyen.DiaChiDung = VanChuyen.DiaChiDung;
            _VanChuyen.Gia = VanChuyen.Gia;
            _VanChuyen.ThoiGian = VanChuyen.ThoiGian;
            _VanChuyen.AnhDaiDien = VanChuyen.AnhDaiDien;
            _VanChuyen.ChiTietDiemDung = VanChuyen.ChiTietDiemDung;
            _VanChuyen.ThoiGianBatDau = VanChuyen.ThoiGianBatDau;
            _VanChuyen.ThoiGianKetThuc = VanChuyen.ThoiGianKetThuc;
            _VanChuyen.TaiXe = VanChuyen.TaiXe;
            _DBContext.VanChuyens.Update(_VanChuyen);
            _DBContext.SaveChanges();
        }

        public async Task<DatVanChuyen> DatVanChuyen(DatVanChuyenVM datVanChuyen)
        {
            DatVanChuyen dat = new DatVanChuyen
            {
                IdNguoiDung = datVanChuyen.IdNguoiDung,
                IdVanChuyen = datVanChuyen.IdVanChuyen,
            };
            await _DBContext.DatVanChuyens.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
            return dat;
        }
    }
}