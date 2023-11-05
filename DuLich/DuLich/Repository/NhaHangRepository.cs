using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.NhaHang;

namespace DemoCrud.Responsitory
{
    public class NhaHangRepository : INhaHangRepository
    {
        private AppDBContext _DBContext;
        private readonly IUploadService _uploadService;

        public NhaHangRepository(AppDBContext dBContext, IUploadService uploadService)
        {
            _DBContext = dBContext;
            _uploadService = uploadService;
        }

        public async Task<List<NhaHang>> GetAll()
        {
            var ds = await _DBContext.NhaHangs.ToListAsync();

            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }

            return ds;
        }

        public async Task<NhaHang> GetNhaHang(int id)
        {
            var ds = await _DBContext.NhaHangs.Include(x => x.NguoiDung).Where(x => x.Id == id).FirstOrDefaultAsync();
            ds.AnhDaiDien = _uploadService.GetFullPath(ds.AnhDaiDien);
            return ds;
        }

        public async Task Add(NhaHangVM nhaHang)
        {
            NhaHang user1 = new NhaHang
            {
                ChuDichVu = nhaHang.ChuDichVu,
                DiaChi = nhaHang.DiaChi,
                Gia = nhaHang.Gia,
                TenNhaHang = nhaHang.TenNhaHang,
                ChiTietNhaHang = nhaHang.ChiTietNhaHang,
                MoTaNhaHang = nhaHang.MoTaNhaHang,
                DanhGia = 0
            };
            if (nhaHang.AnhDaiDien != null)
            {
                user1.AnhDaiDien = await _uploadService.SaveFile(nhaHang.AnhDaiDien);
            }
            await _DBContext.NhaHangs.AddAsync(user1);
            await _DBContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var nhaHang = _DBContext.NhaHangs.SingleOrDefault(lo => lo.Id == id);
            if (nhaHang != null)
            {
                _DBContext.Remove(nhaHang);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task Update(NhaHangVM nhaHang)
        {
            var _nhahang = await _DBContext.NhaHangs.FirstOrDefaultAsync(lo => lo.Id == nhaHang.IdNH);

            if (nhaHang != null)
            {
                _nhahang.ChuDichVu = nhaHang.ChuDichVu;
                _nhahang.DiaChi = nhaHang.DiaChi;
                _nhahang.Gia = nhaHang.Gia;
                _nhahang.TenNhaHang = nhaHang.TenNhaHang;
                _nhahang.ChiTietNhaHang = nhaHang.ChiTietNhaHang;
                _nhahang.MoTaNhaHang = nhaHang.MoTaNhaHang;
                _nhahang.DanhGia = 0;
                string anh = "";
                if (nhaHang.AnhDaiDien != null)
                {
                    anh = _nhahang.AnhDaiDien;
                    _nhahang.AnhDaiDien = await _uploadService.SaveFile(nhaHang.AnhDaiDien);
                }
                _DBContext.NhaHangs.Update(_nhahang);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task DatNhaHang(DatNhaHangVM datNhaHang)
        {
            DatNhaHang dat = new DatNhaHang
            {
                IdNguoiDung = datNhaHang.IdNguoiDungs,
                IdNhaHang = datNhaHang.IdNhaHangs,
                NgayNhan = datNhaHang.NgayNhan,
                NgayTra = datNhaHang.NgayTra,
                NgayDat = DateTime.Now
            };
            await _DBContext.DatNhaHangs.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
        }

        public async Task<List<NhaHang>> GetByOwner(int id)
        {
            var ds = await _DBContext.NhaHangs.Where(x => x.ChuDichVu == id).ToListAsync();

            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }

            return ds;
        }

        public async Task<List<NhaHang>> Search(TimKiemNhaHangRequest request)
        {
            var ds = await _DBContext.NhaHangs.Where(x => x.TenNhaHang.ToLower().Contains(request.Key)
            || x.MoTaNhaHang.ToLower().Contains(request.Key)).ToListAsync();

            foreach (var d in ds)
            {
                d.AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien);
            }

            return ds;
        }

        public async Task<List<DatNhaHang>> GetNhaHangByNguoiDung(int id)
        {
            return await _DBContext.DatNhaHangs.Include(x => x.NhaHang).Where(x => x.IdNguoiDung == id).ToListAsync();
        }
    }
}