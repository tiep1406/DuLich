using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.KhachSan;

namespace DemoCrud.Responsitory
{
    public class KhachSanRepository : IKhachSanRepository
    {
        private AppDBContext _DBContext;
        private readonly IUploadService _uploadService;

        public KhachSanRepository(AppDBContext dBContext, IUploadService uploadService)
        {
            _DBContext = dBContext;
            _uploadService = uploadService;
        }

        public async Task<List<KhachSan>> GetAll()
        {
            var ds = await _DBContext.KhachSans.Select(t => t).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<KhachSan> GetKhachSan(int id)
        {
            var ds = await _DBContext.KhachSans.FindAsync(id);
            ds.AnhDaiDien = _uploadService.GetFullPath(ds.AnhDaiDien);
            return ds;
        }

        public async Task Add(KhachSanVM KhachSan)
        {
            KhachSan user1 = new KhachSan
            {
                ChuDichVu = KhachSan.ChuDichVu,
                DiaChi = KhachSan.DiaChi,
                Gia = KhachSan.Gia,
                TenKhachSan = KhachSan.TenKhachSan,
                ChiTietKhachSan = KhachSan.ChiTietKhachSan,
                MoTaKhachSan = KhachSan.MoTaKhachSan,
                DanhGia = 0
            };
            if (KhachSan.AnhDaiDien != null)
            {
                user1.AnhDaiDien = await _uploadService.SaveFile(KhachSan.AnhDaiDien);
            }
            await _DBContext.KhachSans.AddAsync(user1);
            await _DBContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var KhachSan = _DBContext.KhachSans.SingleOrDefault(lo => lo.Id == id);
            if (KhachSan != null)
            {
                _DBContext.Remove(KhachSan);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task Update(KhachSanVM KhachSan)
        {
            var _KhachSan = _DBContext.KhachSans.SingleOrDefault(lo => lo.Id == KhachSan.IdKS);

            if (KhachSan != null)
            {
                _KhachSan.ChuDichVu = KhachSan.ChuDichVu;
                _KhachSan.DiaChi = KhachSan.DiaChi;
                _KhachSan.Gia = KhachSan.Gia;
                _KhachSan.TenKhachSan = KhachSan.TenKhachSan;
                _KhachSan.ChiTietKhachSan = KhachSan.ChiTietKhachSan;
                _KhachSan.MoTaKhachSan = KhachSan.MoTaKhachSan;
                _KhachSan.DanhGia = 0;
                string anh = "";
                if (KhachSan.AnhDaiDien != null)
                {
                    anh = _KhachSan.AnhDaiDien;
                    _KhachSan.AnhDaiDien = await _uploadService.SaveFile(KhachSan.AnhDaiDien);
                }
                _DBContext.KhachSans.Update(_KhachSan);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task DatKhachSan(DatKhachSanVM datKhachSan)
        {
            DatKhachSan dat = new DatKhachSan
            {
                IdNguoiDung = datKhachSan.IdNguoiDungs,
                IdKhachSan = datKhachSan.IdKhachSans,
                NgayDat = datKhachSan.NgayDat,
                NgayTra = datKhachSan.NgayTra,
            };
            await _DBContext.DatKhachSans.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
        }

        public async Task<List<KhachSan>> Search(TimKiemKhachSanRequest request)
        {
            var ds = await _DBContext.KhachSans.Where(x => x.TenKhachSan.Contains(request.Key)).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<List<KhachSan>> GetByOwner(int id)
        {
            var ds = await _DBContext.KhachSans.Where(x => x.ChuDichVu == id).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }
    }
}