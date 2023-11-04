using RestEase;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.DiemThamQuan;
using ViewModel.Request.NhaHang;

namespace APIIntegration.Interfaces
{
    public interface IRestaurantAPI
    {
        [Get("nhahang")]
        Task<List<NhaHang>> GetAll();

        [Get("nhahang/{id}")]
        Task<NhaHang> GetById([Path("id")] int id);

        [Get("nhahang/owner/{id}")]
        Task<List<NhaHang>> GetNhaHangByOwner([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("nhahang/search")]
        Task<List<NhaHang>> SearchNhaHang([Body] TimKiemNhaHangRequest request);

        [Post("nhahang")]
        Task CreateNhaHang([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("nhahang")]
        Task EditNhaHang([Body] HttpContent content, [Header("Authorization")] string authorization);
    }

    public static class RestaurantAPIExtensions
    {
        public static async Task CreateNhaHang(this IRestaurantAPI api, NhaHangVM request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.DiaChi), "DiaChi" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.TenNhaHang), "TenNhaHang" },
                { new StringContent(request.ChiTietNhaHang), "ChiTietNhaHang" },
                { new StringContent(request.MoTaNhaHang), "MoTaNhaHang" }
            };
            byte[] userBytes;
            if (request.AnhDaiDien != null)
            {
                using (var stream = new BinaryReader(request.AnhDaiDien.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.AnhDaiDien.Length);
                }
                requestContent.Add(new ByteArrayContent(userBytes), "AnhDaiDien", request.AnhDaiDien.FileName);
            }

            await api.CreateNhaHang(requestContent, token);
        }

        public static async Task EditNhaHang(this IRestaurantAPI api, NhaHangVM request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.IdNH.ToString()), "IdNH" },
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.DiaChi), "DiaChi" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.TenNhaHang), "TenNhaHang" },
                { new StringContent(request.ChiTietNhaHang), "ChiTietNhaHang" },
                { new StringContent(request.MoTaNhaHang), "MoTaNhaHang" }
            };
            byte[] userBytes;
            if (request.AnhDaiDien != null)
            {
                using (var stream = new BinaryReader(request.AnhDaiDien.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.AnhDaiDien.Length);
                }
                requestContent.Add(new ByteArrayContent(userBytes), "AnhDaiDien", request.AnhDaiDien.FileName);
            }

            await api.EditNhaHang(requestContent, token);
        }
    }
}