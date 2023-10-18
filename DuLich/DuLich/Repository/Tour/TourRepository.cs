using DuLich.Dtos;
using DuLich.Models;
using DuLich.Repository.DBContext;
using DuLich.Request.Tour;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
            tour.TourCT.TenTour = request.TenTour;

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

        public async Task<TourDto> GetChiTietTour(int id)
        {
            var tour = await _context.Tours
                .Include(x => x.TourCT)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy tour tương ứng");

            return MapToDto(tour);
        }

        public async Task<List<TourDto>> GetDanhSachTour()
        {
            var tours = await _context.Tours
                .Include(x => x.TourCT)
                .ToListAsync();

            var tourDtos = new List<TourDto>();
            foreach (var tour in tours)
            {
                tourDtos.Add(MapToDto(tour));
            }
            return tourDtos;
        }

        public async Task ThemTour(ThemTourRequest request)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == request.ChuTour)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");

            var tour = new Models.Tour()
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
                    LichTrinhNgay = request.LichTrinhNgay,
                    TenTour = request.TenTour
                }
            };

            if (request.HinhAnhTour != null)
            {
                tour.HinhAnhTour = await _uploadService.SaveFile(request.HinhAnhTour);
            }

            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
        }

        private TourDto MapToDto(Models.Tour tour)
        {
            return new TourDto()
            {
                Id = tour.Id,
                ChiTietTour = tour.ChiTietTour,
                ChuTour = tour.ChuTour,
                GiaTour = tour.GiaTour,
                HinhAnhTour = _uploadService.GetFullPath(tour.HinhAnhTour),
                KhuyenMaiTour = tour.KhuyenMaiTour,
                MoTaTour = tour.MoTaTour,
                NgayBatDau = tour.NgayBatDau,
                NgayKetThuc = tour.NgayKetThuc,
                SoNgay = tour.SoNgay,
                TenTour = tour.TenTour,
                LuotDanhGia = tour.LuotDanhGia,
                ChiTietLichTrinh = tour.TourCT.ChiTietLichTrinh,
                LichTrinhNgay = tour.TourCT.LichTrinhNgay
            };
        }

        public async Task<List<TourDto>> TimKiemTour(TimKiemTourRequest request)
        {
            var tours = await _context.Tours
                .Include(x => x.TourCT)
                .Where(x => x.TenTour.ToLower().Contains(request.Key)
                || x.ChiTietTour.ToLower().Contains(request.Key)
                || x.MoTaTour.ToLower().Contains(request.Key))
                .ToListAsync();

            var tourDtos = new List<TourDto>();
            foreach (var tour in tours)
            {
                tourDtos.Add(MapToDto(tour));
            }
            return tourDtos;
        }
    }
}