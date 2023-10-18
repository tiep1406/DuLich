using DuLich.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.OleDb;
using System.Data.SqlClient;
using DuLich.Data;
using DuLich.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace DemoCrud.Responsitory
{
    public class KhachSanRepository : IKhachSanRepositoty
    {
        private ApplicationDBContext _DBContext;
        public KhachSanRepository(ApplicationDBContext dBContext)
        {
            _DBContext = dBContext;
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
            KhachSan user1 = new KhachSan
            {
                ChuDichVu = KhachSan.ChuDichVu,
                DiaChi = KhachSan.DiaChi,
                Gia = KhachSan.Gia,
                TenKhachSan = KhachSan.TenKhachSan,
                AnhDaiDien = KhachSan.AnhDaiDien,
                ChiTietKhachSan = KhachSan.ChiTietKhachSan,
                MoTaKhachSan = KhachSan.MoTaKhachSan,
                DanhGia = KhachSan.DanhGia
            };
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


        public void Update(KhachSanVM KhachSan)
        {
            var _KhachSan = _DBContext.KhachSans.SingleOrDefault(lo => lo.Id == KhachSan.Id);
            _KhachSan.ChuDichVu = KhachSan.ChuDichVu;
            _KhachSan.DiaChi = KhachSan.DiaChi;
            _KhachSan.Gia = KhachSan.Gia;
            _KhachSan.TenKhachSan = KhachSan.TenKhachSan;
            _KhachSan.AnhDaiDien = KhachSan.AnhDaiDien;
            _KhachSan.ChiTietKhachSan = KhachSan.ChiTietKhachSan;
            _KhachSan.MoTaKhachSan = KhachSan.MoTaKhachSan;
            _KhachSan.DanhGia = KhachSan.DanhGia;
            _DBContext.KhachSans.Update(_KhachSan);
            _DBContext.SaveChanges();
        }
    }
}
