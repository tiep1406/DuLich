﻿using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.KhachSan;

namespace DemoCrud.Responsitory
{
    public interface IKhachSanRepository
    {
        Task<KhachSan> GetKhachSan(int id);

        Task<List<KhachSan>> GetAll();

        Task Update(KhachSanVM KhachSan);

        Task Delete(int id);

        Task Toggle(int id);

        Task Add(KhachSanVM KhachSan);

        Task BinhLuan(BinhLuanKhachSanVM binhLuanKhachSan);

        Task DatKhachSan(DatKhachSanVM datKhachSan);

        Task<List<KhachSan>> Search(TimKiemKhachSanRequest request);

        Task<List<KhachSan>> GetByOwner(int id);

        Task<List<ViewModel.Models.DatKhachSan>> GetKhachSanByNguoiDung(int id);
    }
}