using RestEase;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.VanChuyen;

namespace APIIntegration.Interfaces
{
    public interface IDeliveryAPI
    {
        [Get("vanchuyen")]
        Task<List<VanChuyen>> GetAll();

        [Get("vanchuyen/{id}")]
        Task<VanChuyen> GetById([Path("id")] int id);

        [Get("vanchuyen/owner/{id}")]
        Task<List<VanChuyen>> GetVanChuyenByOwner([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("vanchuyen/user/{id}")]
        Task<List<DatVanChuyen>> GetVanChuyenOrder([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("vanchuyen/search")]
        Task<List<VanChuyen>> SearchVanChuyen([Body] TimKiemVanChuyenRequest request);

        [Delete("vanchuyen/{id}")]
        Task<VanChuyen> Delete([Path("id")] int id, [Header("Authorization")] string authorization);

        [Post("vanchuyen")]
        Task CreateVanChuyen([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("vanchuyen")]
        Task EditVanChuyen([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("vanchuyen/toggle/{id}")]
        Task Toggle([Path("id")] int id, [Header("Authorization")] string authorization);

        [Post("vanchuyen/order")]
        Task DatVanChuyen([Body] DatVanChuyenVM content, [Header("Authorization")] string authorization);
    }

    public static class DeliveryAPIExtensions
    {
        public static async Task CreateVanChuyen(this IDeliveryAPI api, VanChuyenVM request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.DiaChiDi), "DiaChiDi" },
                { new StringContent(request.DiaChiDung), "DiaChiDung" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.ThoiGianBatDau.ToString()), "ThoiGianBatDau" },
                { new StringContent(request.ThoiGianKetThuc.ToString()), "ThoiGianKetThuc" },
                { new StringContent(request.TaiXe.ToString()), "TaiXe" },
                { new StringContent(request.ChiTietDiemDi), "ChiTietDiemDi" },
                { new StringContent(request.ChiTietDiemDung), "ChiTietDiemDung" }
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

            await api.CreateVanChuyen(requestContent, token);
        }

        public static async Task EditVanChuyen(this IDeliveryAPI api, VanChuyenVM request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.IdVC.ToString()), "IdVC" },
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.DiaChiDi), "DiaChiDi" },
                { new StringContent(request.DiaChiDung), "DiaChiDung" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.ThoiGianBatDau.ToString()), "ThoiGianBatDau" },
                { new StringContent(request.ThoiGianKetThuc.ToString()), "ThoiGianKetThuc" },
                { new StringContent(request.TaiXe.ToString()), "TaiXe" },
                { new StringContent(request.ChiTietDiemDi), "ChiTietDiemDi" },
                { new StringContent(request.ChiTietDiemDung), "ChiTietDiemDung" }
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

            await api.EditVanChuyen(requestContent, token);
        }
    }
}