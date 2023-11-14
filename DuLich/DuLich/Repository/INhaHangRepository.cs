using System.Data;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.NhaHang;

namespace DemoCrud.Responsitory
{
    public interface INhaHangRepository
    {
        Task<NhaHang> GetNhaHang(int id);

        Task<List<NhaHang>> GetAll();

        Task<List<NhaHang>> Search(TimKiemNhaHangRequest request);

        Task<List<NhaHang>> GetByOwner(int id);

        Task Update(NhaHangVM nhaHang);

        Task Delete(int id);

        Task Toggle(int id);

        Task Add(NhaHangVM nhaHang);

        Task BinhLuan(BinhLuanNhaHangVM binhLuanNhaHang);

        Task DatNhaHang(DatNhaHangVM datNhaHang);

        Task<List<ViewModel.Models.DatNhaHang>> GetNhaHangByNguoiDung(int id);
    }
}