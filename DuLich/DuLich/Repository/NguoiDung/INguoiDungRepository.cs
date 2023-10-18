using DuLich.Dtos;
using DuLich.Request.NguoiDung;

namespace DuLich.Repository.NguoiDung
{
    public interface INguoiDungRepository
    {
        Task<NguoiDungDto> GetNguoiDungById(int id);

        Task<List<NguoiDungDto>> GetDanhSachNguoiDung();

        Task<string> DangNhap(DangNhapRequest request);

        Task DangKy(DangKyRequest request);

        Task ChinhSuaNguoiDung(ChinhSuaNguoiDungRequest request);
    }
}