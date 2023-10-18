using DuLich.Models;
using DuLich.ModelsView;
using System.Data;

namespace DemoCrud.Responsitory
{
    public interface INhaHangRepositoty
    {
        Task<NhaHang> GetNhaHang(int id);
        Task<List<NhaHang>> GetAll();
        void Update(NhaHangVM nhaHang);
        void Delete(int id);
        Task<NhaHang> Add(NhaHangVM nhaHang);
        Task<DatNhaHang> DatNhaHang(DatNhaHangVM datNhaHang);
    }
}
