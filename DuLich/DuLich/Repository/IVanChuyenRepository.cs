using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.KhachSan;
using ViewModel.Request.VanChuyen;

namespace DemoCrud.Responsitory
{
    public interface IVanChuyenRepository
    {
        Task<VanChuyen> GetVanChuyen(int id);

        Task<List<VanChuyen>> GetAll();

        Task Update(VanChuyenVM VanChuyen);

        Task Delete(int id);

        Task Toggle(int id);

        Task Add(VanChuyenVM VanChuyen);

        Task DatVanChuyen(DatVanChuyenVM datVanChuyen);

        Task<List<VanChuyen>> Search(TimKiemVanChuyenRequest request);

        Task<List<VanChuyen>> GetByOwner(int id);

        Task<List<ViewModel.Models.DatVanChuyen>> GetVanChuyenByNguoiDung(int id);
    }
}