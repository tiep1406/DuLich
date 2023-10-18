using DuLich.Dtos;
using DuLich.Request.DiemThamQuan;

namespace DuLich.Repository.DiemThamQuan
{
    public interface IDiemThamQuanRepository
    {
        Task ThemDiemThamQuan(ThemDiemThamQuanRequest request);

        Task ChinhSuaDiemThamQuan(ChinhSuaDiemThamQuanRequest request);

        Task<List<DiemThamQuanDto>> GetDanhSachDiemThamQuan();

        Task<List<DiemThamQuanDto>> TimKiemDiemThamQuan(TimKiemDiemThamQuanRequest request);

        Task<DiemThamQuanDto> GetChiTietDiemThamQuan(int id);
    }
}