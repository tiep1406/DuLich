using DuLich.Models;
using DuLich.ModelsView;
using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoCrud.Responsitory
{
    public class VanChuyenRepository : IVanChuyenRepositoty
    {
        private AppDBContext _DBContext;
        private readonly IUploadService _uploadService;
        public VanChuyenRepository(AppDBContext dBContext, IUploadService uploadService)
        {
            _DBContext = dBContext;
            _uploadService = uploadService;
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
            var user = _DBContext.NguoiDungs.FirstOrDefaultAsync(x => x.Id == VanChuyen.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            VanChuyen user1 = new VanChuyen
            {
                ChuDichVu = VanChuyen.ChuDichVu,
                DiaChiDung = VanChuyen.DiaChiDung,
                Gia = VanChuyen.Gia,
                ThoiGian = VanChuyen.ThoiGian,
                ChiTietDiemDung = VanChuyen.ChiTietDiemDung,
                ThoiGianBatDau = VanChuyen.ThoiGianBatDau,
                ThoiGianKetThuc = VanChuyen.ThoiGianKetThuc,
                TaiXe = VanChuyen.TaiXe
            };
            if (VanChuyen.AnhDaiDien != null)
            {
                user1.AnhDaiDien = await _uploadService.SaveFile(VanChuyen.AnhDaiDien);
            }
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

        public async void Update(VanChuyenVM VanChuyen)
        {
            var _VanChuyen = _DBContext.VanChuyens.SingleOrDefault(lo => lo.Id == VanChuyen.IdVC);
            var user = _DBContext.NguoiDungs.FirstOrDefaultAsync(x => x.Id == VanChuyen.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            if (VanChuyen != null)
            {
                _VanChuyen.ChuDichVu = VanChuyen.ChuDichVu;
                _VanChuyen.DiaChiDung = VanChuyen.DiaChiDung;
                _VanChuyen.Gia = VanChuyen.Gia;
                _VanChuyen.ThoiGian = VanChuyen.ThoiGian;
                _VanChuyen.ChiTietDiemDung = VanChuyen.ChiTietDiemDung;
                _VanChuyen.ThoiGianBatDau = VanChuyen.ThoiGianBatDau;
                _VanChuyen.ThoiGianKetThuc = VanChuyen.ThoiGianKetThuc;
                _VanChuyen.TaiXe = VanChuyen.TaiXe;
                string anh = "";
                if (VanChuyen.AnhDaiDien != null)
                {
                    anh = _VanChuyen.AnhDaiDien;
                    _VanChuyen.AnhDaiDien = await _uploadService.SaveFile(VanChuyen.AnhDaiDien);
                }
                _DBContext.VanChuyens.Update(_VanChuyen);
                _DBContext.SaveChanges();
            }
        }

        public async Task<DatVanChuyen> DatVanChuyen(DatVanChuyenVM datVanChuyen)
        {
            DatVanChuyen dat = new DatVanChuyen
            {
                IdNguoiDung = datVanChuyen.IdNguoiDungs,
                IdVanChuyen = datVanChuyen.IdVanChuyens,
            };
            await _DBContext.DatVanChuyens.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
            return dat;
        }
    }
}