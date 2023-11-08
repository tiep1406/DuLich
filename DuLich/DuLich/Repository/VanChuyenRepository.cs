using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.VanChuyen;

namespace DemoCrud.Responsitory
{
    public class VanChuyenRepository : IVanChuyenRepository
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
            var ds = await _DBContext.VanChuyens.Include(x => x.NguoiDung).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<VanChuyen> GetVanChuyen(int id)
        {
            var ds = await _DBContext.VanChuyens.Include(x => x.NguoiDung).Where(x => x.Id == id).FirstOrDefaultAsync();
            ds.AnhDaiDien = _uploadService.GetFullPath(ds.AnhDaiDien);
            return ds;
        }

        public async Task Add(VanChuyenVM VanChuyen)
        {
            VanChuyen user1 = new VanChuyen
            {
                ChuDichVu = VanChuyen.ChuDichVu,
                DiaChiDung = VanChuyen.DiaChiDung,
                Gia = VanChuyen.Gia,
                ChiTietDiemDung = VanChuyen.ChiTietDiemDung,
                ThoiGianBatDau = VanChuyen.ThoiGianBatDau,
                ThoiGianKetThuc = VanChuyen.ThoiGianKetThuc,
                TaiXe = VanChuyen.TaiXe,
                ChiTietDiemDi = VanChuyen.ChiTietDiemDi,
                DiaChiDi = VanChuyen.DiaChiDi,
            };
            if (VanChuyen.AnhDaiDien != null)
            {
                user1.AnhDaiDien = await _uploadService.SaveFile(VanChuyen.AnhDaiDien);
            }
            await _DBContext.VanChuyens.AddAsync(user1);
            await _DBContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var VanChuyen = _DBContext.VanChuyens.SingleOrDefault(lo => lo.Id == id);
            if (VanChuyen != null)
            {
                _DBContext.Remove(VanChuyen);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task Update(VanChuyenVM VanChuyen)
        {
            var _VanChuyen = _DBContext.VanChuyens.SingleOrDefault(lo => lo.Id == VanChuyen.IdVC);

            if (VanChuyen != null)
            {
                _VanChuyen.ChuDichVu = VanChuyen.ChuDichVu;
                _VanChuyen.DiaChiDung = VanChuyen.DiaChiDung;
                _VanChuyen.Gia = VanChuyen.Gia;
                _VanChuyen.ChiTietDiemDung = VanChuyen.ChiTietDiemDung;
                _VanChuyen.ThoiGianBatDau = VanChuyen.ThoiGianBatDau;
                _VanChuyen.ThoiGianKetThuc = VanChuyen.ThoiGianKetThuc;
                _VanChuyen.TaiXe = VanChuyen.TaiXe;
                _VanChuyen.DiaChiDi = VanChuyen.DiaChiDi;
                _VanChuyen.ChiTietDiemDi = VanChuyen.ChiTietDiemDi;
                string anh = "";
                if (VanChuyen.AnhDaiDien != null)
                {
                    anh = _VanChuyen.AnhDaiDien;
                    _VanChuyen.AnhDaiDien = await _uploadService.SaveFile(VanChuyen.AnhDaiDien);
                }
                _DBContext.VanChuyens.Update(_VanChuyen);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task DatVanChuyen(DatVanChuyenVM datVanChuyen)
        {
            DatVanChuyen dat = new DatVanChuyen
            {
                IdNguoiDung = datVanChuyen.IdNguoiDungs,
                IdVanChuyen = datVanChuyen.IdVanChuyens,
                NgayDat = DateTime.Now,
            };
            await _DBContext.DatVanChuyens.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
        }

        public async Task<List<VanChuyen>> Search(TimKiemVanChuyenRequest request)
        {
            var ds = await _DBContext.VanChuyens.Where(x => x.DiaChiDi.ToLower().Contains(request.Key) && x.TrangThai == true).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<List<VanChuyen>> GetByOwner(int id)
        {
            var ds = await _DBContext.VanChuyens
                .Include(x => x.DatVanChuyens)
                .ThenInclude(x => x.NguoiDung)
                //.Include(x => x.DatVanChuyens)
                //.ThenInclude(x => x.VanChuyen)
                .Where(x => x.ChuDichVu == id).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<List<DatVanChuyen>> GetVanChuyenByNguoiDung(int id)
        {
            return await _DBContext.DatVanChuyens.Include(x => x.VanChuyen).Where(x => x.IdNguoiDung == id).ToListAsync();
        }

        public async Task Toggle(int id)
        {
            var vanchuyen = _DBContext.VanChuyens.SingleOrDefault(lo => lo.Id == id);
            if (vanchuyen != null)
            {
                vanchuyen.TrangThai = !vanchuyen.TrangThai;
                _DBContext.Update(vanchuyen);
                await _DBContext.SaveChangesAsync();
            }
        }
    }
}