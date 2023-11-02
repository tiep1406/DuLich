using RestEase;
using ViewModel.ModelsView;
using ViewModel.Request.NguoiDung;

namespace APIIntegration.Interfaces
{
    public interface IAuthAPI
    {
        [Post("auth/login")]
        Task<AuthResponse> DangNhap([Body] DangNhapRequest request);

        [Post("auth/register")]
        Task<object> DangKy([Body] DangKyRequest request);
    }
}