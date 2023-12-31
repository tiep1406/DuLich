﻿using DuLich.Repository.DBContext;
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
            var ds = await _DBContext.KhachSans.Include(x => x.NguoiDung).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<KhachSan> GetKhachSan(int id)
        {
            var ds = await _DBContext.KhachSans
                .Include(x => x.NguoiDung)
                .Include(x => x.BinhLuanKhachSans)
                .ThenInclude(x => x.NguoiDung)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            ds.AnhDaiDien = _uploadService.GetFullPath(ds.AnhDaiDien);
            foreach (var t in ds.BinhLuanKhachSans)
            {
                var x = _uploadService.GetFullPath(t.NguoiDung.AnhDaiDien);
                if (!t.NguoiDung.AnhDaiDien.Contains("/"))
                    t.NguoiDung.AnhDaiDien = x;
            }
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
                NgayDat = DateTime.Now,
                NgayNhan = datKhachSan.NgayNhan,
                NgayTra = datKhachSan.NgayTra,
            };
            await _DBContext.DatKhachSans.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
        }

        public async Task<List<KhachSan>> Search(TimKiemKhachSanRequest request)
        {
            var ds = await _DBContext.KhachSans.Where(x => x.TenKhachSan.Contains(request.Key) && x.TrangThai == true).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<List<KhachSan>> GetByOwner(int id)
        {
            var ds = await _DBContext.KhachSans
                .Include(x => x.DatKhachSans)
                .ThenInclude(x => x.NguoiDung)
                //.Include(x => x.DatKhachSans)
                //.ThenInclude(x => x.KhachSan)
                .Where(x => x.ChuDichVu == id).ToListAsync();
            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }
            return ds;
        }

        public async Task<List<DatKhachSan>> GetKhachSanByNguoiDung(int id)
        {
            return await _DBContext.DatKhachSans.Include(x => x.KhachSan).Where(x => x.IdNguoiDung == id).ToListAsync();
        }

        public async Task Toggle(int id)
        {
            var KhachSan = _DBContext.KhachSans.SingleOrDefault(lo => lo.Id == id);
            if (KhachSan != null)
            {
                KhachSan.TrangThai = !KhachSan.TrangThai;
                _DBContext.Update(KhachSan);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task BinhLuan(BinhLuanKhachSanVM binhLuanKhachSan)
        {
            var bl = new BinhLuanKhachSan()
            {
                IdKhachSan = binhLuanKhachSan.IdKhachSan,
                IdNguoiDung = binhLuanKhachSan.IdNguoiDung,
                NoiDung = binhLuanKhachSan.NoiDung,
                Rating = binhLuanKhachSan.Rating,
                ThoiGian = DateTime.Now
            };
            await _DBContext.BinhLuanKhachSans.AddAsync(bl);
            await _DBContext.SaveChangesAsync();
        }
    }
}