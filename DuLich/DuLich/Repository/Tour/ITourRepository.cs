using DuLich.Dtos;
using DuLich.Request.Tour;

namespace DuLich.Repository.Tour
{
    public interface ITourRepository
    {
        Task DatTour(DatTourRequest request);

        Task ThemTour(ThemTourRequest request);

        Task ChinhSuaTour(ChinhSuaTourRequest request);

        Task XoaTour(int id);

        Task<List<TourDto>> GetDanhSachTour();

        Task<List<TourDto>> GetDanhSachTourByChuDichVu(int id);

        Task<List<TourDto>> GetDanhSachTourByNguoiDung(int id);

        Task<List<TourDto>> TimKiemTour(TimKiemTourRequest request);

        Task<TourDto> GetChiTietTour(int id);
    }
}