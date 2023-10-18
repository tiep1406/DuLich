using DuLich.Dtos;
using DuLich.Request.Tour;

namespace DuLich.Repository.Tour
{
    public interface ITourRepository
    {
        Task DatTour(DatTourRequest request);

        Task ThemTour(ThemTourRequest request);

        Task ChinhSuaTour(ChinhSuaTourRequest request);

        Task<List<TourDto>> GetDanhSachTour();

        Task<List<TourDto>> TimKiemTour(TimKiemTourRequest request);

        Task<TourDto> GetChiTietTour(int id);
    }
}