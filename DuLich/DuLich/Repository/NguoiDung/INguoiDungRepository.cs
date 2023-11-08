using ViewModel.ModelsView;
using ViewModel.Request.NguoiDung;

namespace DuLich.Repository.NguoiDung
{
    public interface INguoiDungRepository
    {
        Task<ViewModel.Models.NguoiDung> GetNguoiDungById(int id);

        Task Toggle(int id);

        Task<List<ViewModel.Models.NguoiDung>> GetDanhSachNguoiDung();

        Task<AuthResponse> DangNhap(DangNhapRequest request);

        Task DangKy(DangKyRequest request);

        Task ChinhSuaNguoiDung(ChinhSuaNguoiDungRequest request);
    }
}