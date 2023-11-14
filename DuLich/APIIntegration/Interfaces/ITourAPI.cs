using RestEase;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.Tour;

namespace APIIntegration.Interfaces
{
    public interface ITourAPI
    {
        [Get("tours")]
        Task<List<Tour>> GetAll();

        [Get("tours/{id}")]
        Task<Tour> GetById([Path("id")] int id);

        [Get("tours/search")]
        Task<List<Tour>> SearchTour([Body] TimKiemTourRequest request);

        [Get("tours/user/{id}")]
        Task<List<DatTour>> GetTourByUser([Path("id")] int id, [Header("Authorization")] string authorization);

        [Get("tours/owner/{id}")]
        Task<List<Tour>> GetTourByOwner([Path("id")] int id, [Header("Authorization")] string authorization);

        [Post("tours")]
        Task CreateTour([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Put("tours")]
        Task EditTour([Body] HttpContent content, [Header("Authorization")] string authorization);

        [Post("tours/order")]
        Task DatTour([Body] DatTourRequest request, [Header("Authorization")] string authorization);

        [Put("tours/toggle/{id}")]
        Task Toggle([Path("id")] int id, [Header("Authorization")] string authorization);

        [Post("tours/binhluan")]
        Task BinhLuanTour([Body] BinhLuanTourVM request, [Header("Authorization")] string authorization);
    }

    public static class TourAPIExtensions
    {
        public static async Task CreateTour(this ITourAPI api, ThemTourRequest request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.ChuTour.ToString()), "ChuTour" },
                { new StringContent(request.TenTour), "TenTour" },
                { new StringContent(request.SoNgay.ToString()), "SoNgay" },
                { new StringContent(request.ChiTietTour), "ChiTietTour" },
                { new StringContent(request.KhuyenMaiTour.ToString()), "KhuyenMaiTour" },
                { new StringContent(request.MoTaTour), "MoTaTour" },
                { new StringContent(request.NgayBatDau.ToString()), "NgayBatDau" },
                { new StringContent(request.NgayKetThuc.ToString()), "NgayKetThuc" },
                { new StringContent(request.LichTrinhNgay), "LichTrinhNgay" },
                { new StringContent(request.DiaDiem), "DiaDiem" },
                { new StringContent(request.ChiTietLichTrinh), "ChiTietLichTrinh" },
                { new StringContent(request.GiaTour.ToString()), "GiaTour" },
            };
            byte[] userBytes;
            string fileName = "";
            if (request.HinhAnhTour != null)
            {
                using (var stream = new BinaryReader(request.HinhAnhTour.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.HinhAnhTour.Length);
                }
                fileName = request.HinhAnhTour.FileName;
                requestContent.Add(new ByteArrayContent(userBytes), "HinhAnhTour", fileName);
            }

            await api.CreateTour(requestContent, token);
        }

        public static async Task EditTour(this ITourAPI api, ChinhSuaTourRequest request, string token)
        {
            var requestContent = new MultipartFormDataContent
            {
                { new StringContent(request.Id.ToString()), "Id" },
                { new StringContent(request.ChuTour.ToString()), "ChuTour" },
                { new StringContent(request.TenTour), "TenTour" },
                { new StringContent(request.SoNgay.ToString()), "SoNgay" },
                { new StringContent(request.ChiTietTour), "ChiTietTour" },
                { new StringContent(request.KhuyenMaiTour.ToString()), "KhuyenMaiTour" },
                { new StringContent(request.MoTaTour), "MoTaTour" },
                { new StringContent(request.NgayBatDau.ToString()), "NgayBatDau" },
                { new StringContent(request.NgayKetThuc.ToString()), "NgayKetThuc" },
                { new StringContent(request.LichTrinhNgay), "LichTrinhNgay" },
                { new StringContent(request.DiaDiem), "DiaDiem" },
                { new StringContent(request.ChiTietLichTrinh), "ChiTietLichTrinh" },
                { new StringContent(request.GiaTour.ToString()), "GiaTour" },
            };
            byte[] userBytes;
            string fileName = "";
            if (request.HinhAnhTour != null)
            {
                using (var stream = new BinaryReader(request.HinhAnhTour.OpenReadStream()))
                {
                    userBytes = stream.ReadBytes((int)request.HinhAnhTour.Length);
                }
                fileName = request.HinhAnhTour.FileName;
                requestContent.Add(new ByteArrayContent(userBytes), "HinhAnhTour", fileName);
            }

            await api.EditTour(requestContent, token);
        }
    }
}