﻿using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.Tour;

namespace DuLich.Repository.Tour
{
    public class TourRepository : ITourRepository
    {
        private readonly AppDBContext _context;
        private readonly IUploadService _uploadService;

        public TourRepository(AppDBContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public async Task ChinhSuaTour(ChinhSuaTourRequest request)
        {
            var tour = await _context.Tours
                .Include(x => x.TourCT)
                .FirstOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new KeyNotFoundException("Không tìm thấy tour tương ứng");

            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == request.ChuTour)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");

            tour.ChiTietTour = request.ChiTietTour;
            tour.ChuTour = request.ChuTour;
            tour.GiaTour = request.GiaTour;
            tour.KhuyenMaiTour = request.KhuyenMaiTour;
            tour.MoTaTour = request.MoTaTour;
            tour.NgayBatDau = request.NgayBatDau;
            tour.NgayKetThuc = request.NgayKetThuc;
            tour.SoNgay = request.SoNgay;
            tour.TenTour = request.TenTour;
            string anhTour = "";
            if (request.HinhAnhTour != null)
            {
                anhTour = tour.HinhAnhTour;
                tour.HinhAnhTour = await _uploadService.SaveFile(request.HinhAnhTour);
            }

            tour.TourCT.ChuTour = request.ChuTour;
            tour.TourCT.ChiTietLichTrinh = request.ChiTietLichTrinh;
            tour.TourCT.LichTrinhNgay = request.LichTrinhNgay;
            tour.TourCT.DiaDiem = request.DiaDiem;

            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(anhTour))
            {
                await _uploadService.DeleteFile(anhTour);
            }
        }

        public async Task DatTour(DatTourRequest request)
        {
            var datTour = new DatTour()
            {
                IdNguoiDung = request.IdNguoiDung,
                IdTour = request.IdTour,
                NgayDat = DateTime.Now,
            };

            await _context.DatTours.AddAsync(datTour);
            await _context.SaveChangesAsync();
        }

        public async Task<ViewModel.Models.Tour> GetChiTietTour(int id)
        {
            var tour = await _context.Tours
                .Include(x => x.TourCT)
                .Include(x => x.NguoiDung)
                .Include(x => x.BinhLuanTours)
                .ThenInclude(x => x.NguoiDung)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy tour tương ứng");
            tour.HinhAnhTour = _uploadService.GetFullPath(tour.HinhAnhTour);
            foreach (var t in tour.BinhLuanTours)
            {
                var x = _uploadService.GetFullPath(t.NguoiDung.AnhDaiDien);
                if (!t.NguoiDung.AnhDaiDien.Contains("/"))
                    t.NguoiDung.AnhDaiDien = x;
            }
            return tour;
        }

        public async Task<List<ViewModel.Models.Tour>> GetDanhSachTour()
        {
            var tours = await _context.Tours
                .Include(x => x.TourCT)
                .Include(x => x.NguoiDung)
                .ToListAsync();
            foreach (var tour in tours)
            {
                tour.HinhAnhTour = _uploadService.GetFullPath(tour.HinhAnhTour);
            }
            return tours;
        }

        public async Task ThemTour(ThemTourRequest request)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == request.ChuTour)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");

            var tour = new ViewModel.Models.Tour()
            {
                ChiTietTour = request.ChiTietTour,
                ChuTour = request.ChuTour,
                GiaTour = request.GiaTour,
                KhuyenMaiTour = request.KhuyenMaiTour,
                MoTaTour = request.MoTaTour,
                NgayBatDau = request.NgayBatDau,
                NgayKetThuc = request.NgayKetThuc,
                SoNgay = request.SoNgay,
                TenTour = request.TenTour,
                TourCT = new TourCT()
                {
                    ChiTietLichTrinh = request.ChiTietLichTrinh,
                    ChuTour = request.ChuTour,
                    DiaDiem = request.DiaDiem,
                    LichTrinhNgay = request.LichTrinhNgay
                }
            };

            if (request.HinhAnhTour != null)
            {
                tour.HinhAnhTour = await _uploadService.SaveFile(request.HinhAnhTour);
            }

            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ViewModel.Models.Tour>> TimKiemTour(TimKiemTourRequest request)
        {
            var tours = await _context.Tours
                .Include(x => x.TourCT)
                .Where(x => (x.TenTour.ToLower().Contains(request.Key)
                || x.ChiTietTour.ToLower().Contains(request.Key)
                || x.MoTaTour.ToLower().Contains(request.Key)) && x.TrangThai == true)
                .ToListAsync();
            foreach (var tour in tours)
            {
                tour.HinhAnhTour = _uploadService.GetFullPath(tour.HinhAnhTour);
            }
            return tours;
        }

        public async Task<List<ViewModel.Models.Tour>> GetDanhSachTourByChuDichVu(int id)
        {
            var tours = await _context.Tours
                .Include(x => x.TourCT)
                .Include(x => x.DatTours)
                .ThenInclude(x => x.NguoiDung)
                //.Include(x => x.DatTours)
                //.ThenInclude(x => x.Tour)
                //.ThenInclude(x => x.TourCT)
                .Where(x => x.ChuTour == id)
                .ToListAsync();
            foreach (var tour in tours)
            {
                tour.HinhAnhTour = _uploadService.GetFullPath(tour.HinhAnhTour);
            }
            return tours;
        }

        public async Task<List<ViewModel.Models.DatTour>> GetDanhSachTourByNguoiDung(int id)
        {
            var tours = await _context.DatTours
                .Include(x => x.Tour)
                .ThenInclude(x => x.TourCT)
                .Where(x => x.IdNguoiDung == id)
                .ToListAsync();

            foreach (var tour in tours)
            {
                tour.Tour.HinhAnhTour = _uploadService.GetFullPath(tour.Tour.HinhAnhTour);
            }
            return tours;
        }

        public async Task XoaTour(int id)
        {
            var tour = await _context.Tours
                .Include(x => x.TourCT)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy tour tương ứng");

            _context.TourCTs.Remove(tour.TourCT);
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
        }

        public async Task Toggle(int id)
        {
            var tour = await _context.Tours
                .Include(x => x.TourCT)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy tour tương ứng");
            tour.TrangThai = !tour.TrangThai;
            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
        }

        public async Task BinhLuan(BinhLuanTourVM binhLuanTour)
        {
            var bl = new BinhLuanTour()
            {
                IdTour = binhLuanTour.IdTour,
                IdNguoiDung = binhLuanTour.IdNguoiDung,
                NoiDung = binhLuanTour.NoiDung,
                Rating = binhLuanTour.Rating,
                ThoiGian = DateTime.Now
            };

            await _context.BinhLuanTours.AddAsync(bl);
            await _context.SaveChangesAsync();
        }
    }
}