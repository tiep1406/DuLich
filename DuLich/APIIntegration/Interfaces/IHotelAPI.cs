using RestEase;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.KhachSan;

namespace APIIntegration.Interfaces
{
    public interface IHotelAPI
    {
        [Get("khachsan")]
        Task<List<KhachSan>> GetAll();

        [Get("khachsan/{id}")]
        Task<KhachSan> GetById([Path("id")] int id);

        [Get("khachsan/owner/{id}")]
        Task<List<KhachSan>> GetKhachSanByOwner([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("khachsan/user/{id}")]
        Task<List<DatKhachSan>> GetKhachSanOrder([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("khachsan/search")]
        Task<List<KhachSan>> SearchKhachSan([Body] TimKiemKhachSanRequest request);

        [Delete("khachsan/{id}")]
        Task<KhachSan> Delete([Path("id")] int id, [Header("Authorization")] string authorization);

        [Post("khachsan")]
        Task CreateKhachSan([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("khachsan")]
        Task EditKhachSan([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Post("khachsan/order")]
        Task DatKhachSan([Body] DatKhachSanVM request, [Header("Authorization")] string authorization);

        [Put("khachsan/toggle/{id}")]
        Task Toggle([Path("id")] int id, [Header("Authorization")] string authorization);
    }

    public static class HotelAPIExtensions
    {
        public static async Task CreateKhachSan(this IHotelAPI api, KhachSanVM request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.DiaChi), "DiaChi" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.TenKhachSan), "TenKhachSan" },
                { new StringContent(request.ChiTietKhachSan), "ChiTietKhachSan" },
                { new StringContent(request.MoTaKhachSan), "MoTaKhachSan" }
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

            await api.CreateKhachSan(requestContent, token);
        }

        public static async Task EditKhachSan(this IHotelAPI api, KhachSanVM request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.IdKS.ToString()), "IdKS" },
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.DiaChi), "DiaChi" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.TenKhachSan), "TenKhachSan" },
                { new StringContent(request.ChiTietKhachSan), "ChiTietKhachSan" },
                { new StringContent(request.MoTaKhachSan), "MoTaKhachSan" }
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

            await api.EditKhachSan(requestContent, token);
        }
    }
}