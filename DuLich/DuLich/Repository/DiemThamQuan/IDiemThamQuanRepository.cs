using ViewModel.Request.DiemThamQuan;

namespace DuLich.Repository.DiemThamQuan
{
    public interface IDiemThamQuanRepository
    {
        Task ThemDiemThamQuan(ThemDiemThamQuanRequest request);

        Task ChinhSuaDiemThamQuan(ChinhSuaDiemThamQuanRequest request);

        Task XoaDiemThamQuan(int id);

        Task<List<ViewModel.Models.DiemThamQuan>> GetDanhSachDiemThamQuan();

        Task<List<ViewModel.Models.DiemThamQuan>> GetDanhSachDiemThamQuanByChuDichVu(int idChuDichVu);

        Task<List<ViewModel.Models.DiemThamQuan>> TimKiemDiemThamQuan(TimKiemDiemThamQuanRequest request);

        Task<ViewModel.Models.DiemThamQuan> GetChiTietDiemThamQuan(int id);
    }
}