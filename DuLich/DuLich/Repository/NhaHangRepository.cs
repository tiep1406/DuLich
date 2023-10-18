using DuLich.Models;
using DuLich.ModelsView;
using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoCrud.Responsitory
{
    public class NhaHangRepository : INhaHangRepositoty
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
            var user = _DBContext.NguoiDungs.FirstOrDefaultAsync(x => x.Id == nhaHang.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            NhaHang user1 = new NhaHang
            {
                ChuDichVu = nhaHang.ChuDichVu,
                DiaChi = nhaHang.DiaChi,
                Gia = nhaHang.Gia,
                TenNhaHang = nhaHang.TenNhaHang,
                ChiTietNhaHang = nhaHang.ChiTietNhaHang,
                MoTaNhaHang = nhaHang.MoTaNhaHang,
                DanhGia = nhaHang.DanhGia
            };
            if (nhaHang.AnhDaiDien != null)
            {
                user1.AnhDaiDien = await _uploadService.SaveFile(nhaHang.AnhDaiDien);
            }
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

        public async void Update(NhaHangVM nhaHang)
        {
            var _nhahang = _DBContext.NhaHangs.SingleOrDefault(lo => lo.Id == nhaHang.IdNH);
            var user = _DBContext.NguoiDungs.FirstOrDefaultAsync(x => x.Id == nhaHang.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            if (nhaHang != null)
            {
                _nhahang.ChuDichVu = nhaHang.ChuDichVu;
                _nhahang.DiaChi = nhaHang.DiaChi;
                _nhahang.Gia = nhaHang.Gia;
                _nhahang.TenNhaHang = nhaHang.TenNhaHang;
                _nhahang.ChiTietNhaHang = nhaHang.ChiTietNhaHang;
                _nhahang.MoTaNhaHang = nhaHang.MoTaNhaHang;
                _nhahang.DanhGia = nhaHang.DanhGia;
                string anh = "";
                if (nhaHang.AnhDaiDien != null)
                {
                    anh = _nhahang.AnhDaiDien;
                    _nhahang.AnhDaiDien = await _uploadService.SaveFile(nhaHang.AnhDaiDien);
                }
                _DBContext.NhaHangs.Update(_nhahang);
                _DBContext.SaveChanges();
            }
        }

        public async Task<DatNhaHang> DatNhaHang(DatNhaHangVM datNhaHang)
        {
            DatNhaHang dat = new DatNhaHang
            {
                IdNguoiDung = datNhaHang.IdNguoiDungs,
                IdNhaHang = datNhaHang.IdNhaHangs,
                NgayDat = datNhaHang.NgayDat,
                NgayTra = datNhaHang.NgayTra,
            };
            await _DBContext.DatNhaHangs.AddAsync(dat);
            await _DBContext.SaveChangesAsync();
            return dat;
        }
    }
}