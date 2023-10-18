using DuLich.Models;
using DuLich.ModelsView;
using System.Data;

namespace DemoCrud.Responsitory
{
    public interface IKhachSanRepositoty
    {
        Task<KhachSan> GetKhachSan(int id);
        Task<List<KhachSan>> GetAll();
        void Update(KhachSanVM KhachSan);
        void Delete(int id);
        Task<KhachSan> Add(KhachSanVM KhachSan);
    }
}
