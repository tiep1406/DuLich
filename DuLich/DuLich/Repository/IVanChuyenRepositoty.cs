using ViewModel.Models;
using ViewModel.ModelsView;

namespace DemoCrud.Responsitory
{
    public interface IVanChuyenRepositoty
    {
        Task<VanChuyen> GetVanChuyen(int id);

        Task<List<VanChuyen>> GetAll();

        void Update(VanChuyenVM VanChuyen);

        void Delete(int id);

        Task<VanChuyen> Add(VanChuyenVM VanChuyen);

        Task<DatVanChuyen> DatVanChuyen(DatVanChuyenVM datVanChuyen);
    }
}