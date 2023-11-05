using ViewModel.Request.Tour;

namespace DuLich.Repository.Tour
{
    public interface ITourRepository
    {
        Task DatTour(DatTourRequest request);

        Task ThemTour(ThemTourRequest request);

        Task ChinhSuaTour(ChinhSuaTourRequest request);

        Task XoaTour(int id);

        Task<List<ViewModel.Models.Tour>> GetDanhSachTour();

        Task<List<ViewModel.Models.Tour>> GetDanhSachTourByChuDichVu(int id);

        Task<List<ViewModel.Models.DatTour>> GetDanhSachTourByNguoiDung(int id);

        Task<List<ViewModel.Models.Tour>> TimKiemTour(TimKiemTourRequest request);

        Task<ViewModel.Models.Tour> GetChiTietTour(int id);
    }
}