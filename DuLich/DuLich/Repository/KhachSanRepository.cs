using DuLich.Models;
using DuLich.ModelsView;
using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DemoCrud.Responsitory
{
    public class KhachSanRepository : IKhachSanRepositoty
    {
        private AppDBContext _DBContext;
        private readonly IUploadService _uploadService;
        public KhachSanRepository(AppDBContext dBContext,IUploadService uploadService)
        {
            _DBContext = dBContext;
            _uploadService = uploadService;
        }

        public async Task<List<KhachSan>> GetAll()
        {
            var ds = await _DBContext.KhachSans.Select(t => t).ToListAsync();
            return ds;
        }

        public async Task<KhachSan> GetKhachSan(int id)
        {
            var ds = await _DBContext.KhachSans.FindAsync(id);
            return ds;
        }

        public async Task<KhachSan> Add(KhachSanVM KhachSan)
        {
            var user = _DBContext.NguoiDungs.FirstOrDefaultAsync(x => x.Id == KhachSan.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            KhachSan user1 = new KhachSan
            {
                ChuDichVu = KhachSan.ChuDichVu,
                DiaChi = KhachSan.DiaChi,
                Gia = KhachSan.Gia,
                TenKhachSan = KhachSan.TenKhachSan,
                ChiTietKhachSan = KhachSan.ChiTietKhachSan,
                MoTaKhachSan = KhachSan.MoTaKhachSan,
                DanhGia = KhachSan.DanhGia
            };
            if (KhachSan.AnhDaiDien != null)
            {
                user1.AnhDaiDien = await _uploadService.SaveFile(KhachSan.AnhDaiDien);
            }
            await _DBContext.KhachSans.AddAsync(user1);
            await _DBContext.SaveChangesAsync();
            return user1;
        }

        public void Delete(int id)
        {
            var KhachSan = _DBContext.KhachSans.SingleOrDefault(lo => lo.Id == id);
            if (KhachSan != null)
            {
                _DBContext.Remove(KhachSan);
                _DBContext.SaveChanges();
            }
        }

        public async void Update(KhachSanVM KhachSan)
        {
            var _KhachSan = _DBContext.KhachSans.SingleOrDefault(lo => lo.Id == KhachSan.IdKS);
            var user = _DBContext.NguoiDungs.FirstOrDefaultAsync(x => x.Id == KhachSan.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            if (KhachSan != null)
            {
                _KhachSan.ChuDichVu = KhachSan.ChuDichVu;
                _KhachSan.DiaChi = KhachSan.DiaChi;
                _KhachSan.Gia = KhachSan.Gia;
                _KhachSan.TenKhachSan = KhachSan.TenKhachSan;
                _KhachSan.ChiTietKhachSan = KhachSan.ChiTietKhachSan;
                _KhachSan.MoTaKhachSan = KhachSan.MoTaKhachSan;
                _KhachSan.DanhGia = KhachSan.DanhGia;
                string anh = "";
                if (KhachSan.AnhDaiDien != null)
                {
                    anh = _KhachSan.AnhDaiDien;
                    _KhachSan.AnhDaiDien = await _uploadService.SaveFile(KhachSan.AnhDaiDien);
                }
                _DBContext.KhachSans.Update(_KhachSan);
                _DBContext.SaveChanges();
            }
        }

        public async Task<DatKhachSan> DatKhachSan(DatKhachSanVM datKhachSan)
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
            return dat;
        }
    }
}