using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.DiemThamQuan;
using ViewModel.Request.Tour;

namespace APIIntegration.Interfaces
{
    public interface IDestinationAPI
    {
        [Get("travelDestinations")]
        Task<List<DiemThamQuan>> GetAll();

        [Get("travelDestinations/{id}")]
        Task<DiemThamQuan> GetById([Path("id")] int id);

        [Get("travelDestinations/owner/{id}")]
        Task<List<DiemThamQuan>> GetDiemThamQuanByOwner([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("travelDestinations/search")]
        Task<List<DiemThamQuan>> SearchDiemThamQuan([Body] TimKiemDiemThamQuanRequest request);

        [Post("travelDestinations/binhluan")]
        Task BinhLuanDiemThamQuan([Body] BinhLuanDiemThamQuanVM request, [Header("Authorization")] string authorization);

        [Post("travelDestinations")]
        Task CreateDiemThamQuan([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("travelDestinations")]
        Task EditDiemThamQuan([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("travelDestinations/toggle/{id}")]
        Task Toggle([Path("id")] int id, [Header("Authorization")] string authorization);
    }

    public static class DestinationAPIExtensions
    {
        public static async Task CreateDiemThamQuan(this IDestinationAPI api, ThemDiemThamQuanRequest request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.TenDiaDiem), "TenDiaDiem" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.DiaDiem), "DiaDiem" },
                { new StringContent(request.MoTaDichVu), "MoTaDichVu" }
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

            if (request.AnhChiTiet != null)
            {
                using (var stream = new BinaryReader(request.AnhChiTiet.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.AnhChiTiet.Length);
                }
                requestContent.Add(new ByteArrayContent(userBytes), "AnhChiTiet", request.AnhChiTiet.FileName);
            }

            await api.CreateDiemThamQuan(requestContent, token);
        }

        public static async Task EditDiemThamQuan(this IDestinationAPI api, ChinhSuaDiemThamQuanRequest request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.Id.ToString()), "Id" },
                { new StringContent(request.ChuDichVu.ToString()), "ChuDichVu" },
                { new StringContent(request.TenDiaDiem), "TenDiaDiem" },
                { new StringContent(request.Gia.ToString()), "Gia" },
                { new StringContent(request.DiaDiem), "DiaDiem" },
                { new StringContent(request.MoTaDichVu), "MoTaDichVu" }
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

            if (request.AnhChiTiet != null)
            {
                using (var stream = new BinaryReader(request.AnhChiTiet.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.AnhChiTiet.Length);
                }
                requestContent.Add(new ByteArrayContent(userBytes), "AnhChiTiet", request.AnhChiTiet.FileName);
            }

            await api.EditDiemThamQuan(requestContent, token);
        }
    }
}