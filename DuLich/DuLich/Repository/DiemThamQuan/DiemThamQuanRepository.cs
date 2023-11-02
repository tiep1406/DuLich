using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using ViewModel.Request.DiemThamQuan;

namespace DuLich.Repository.DiemThamQuan
{
    public class DiemThamQuanRepository : IDiemThamQuanRepository
    {
        private readonly AppDBContext _context;
        private readonly IUploadService _uploadService;

        public DiemThamQuanRepository(AppDBContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public async Task ChinhSuaDiemThamQuan(ChinhSuaDiemThamQuanRequest request)
        {
            var dtq = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .FirstOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new KeyNotFoundException("Không tìm thấy điểm tham quan tương ứng");

            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == request.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");

            dtq.TenDiaDiem = request.TenDiaDiem;
            dtq.ChuDichVu = request.ChuDichVu;
            dtq.DiaDiem = request.DiaDiem;
            dtq.Gia = request.Gia;
            dtq.DiemThamQuanCT.MoTaDichVu = request.MoTaDichVu;
            string anhDaiDien = "";
            string anhChiTiet = "";
            if (request.AnhDaiDien != null)
            {
                anhDaiDien = dtq.AnhDaiDien;
                dtq.AnhDaiDien = await _uploadService.SaveFile(request.AnhDaiDien);
            }
            if (request.AnhChiTiet != null)
            {
                anhChiTiet = dtq.DiemThamQuanCT.AnhChiTiet;
                dtq.DiemThamQuanCT.AnhChiTiet = await _uploadService.SaveFile(request.AnhChiTiet);
            }
            _context.DiemThamQuans.Update(dtq);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(anhDaiDien))
            {
                await _uploadService.DeleteFile(anhDaiDien);
            }
            if (!string.IsNullOrEmpty(anhChiTiet))
            {
                await _uploadService.DeleteFile(anhChiTiet);
            }
        }

        public async Task<ViewModel.Models.DiemThamQuan> GetChiTietDiemThamQuan(int id)
        {
            var dtq = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy điểm tham quan tương ứng");

            return dtq;
        }

        public async Task<List<ViewModel.Models.DiemThamQuan>> GetDanhSachDiemThamQuan()
        {
            var dtqs = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .ToListAsync();

            return dtqs;
        }

        public async Task ThemDiemThamQuan(ThemDiemThamQuanRequest request)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == request.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            var dtq = new ViewModel.Models.DiemThamQuan()
            {
                ChuDichVu = request.ChuDichVu,
                DiaDiem = request.DiaDiem,
                Gia = request.Gia,
                TenDiaDiem = request.TenDiaDiem,
                DiemThamQuanCT = new ViewModel.Models.DiemThamQuanCT()
                {
                    MoTaDichVu = request.MoTaDichVu,
                    DanhGia = string.Empty
                }
            };
            if (request.AnhChiTiet != null)
            {
                dtq.DiemThamQuanCT.AnhChiTiet = await _uploadService.SaveFile(request.AnhChiTiet);
            }
            if (request.AnhDaiDien != null)
            {
                dtq.AnhDaiDien = await _uploadService.SaveFile(request.AnhDaiDien);
            }

            await _context.DiemThamQuans.AddAsync(dtq);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ViewModel.Models.DiemThamQuan>> TimKiemDiemThamQuan(TimKiemDiemThamQuanRequest request)
        {
            var dtqs = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .Where(x => x.TenDiaDiem.ToLower().Contains(request.Key)
                || x.DiemThamQuanCT.MoTaDichVu.ToLower().Contains(request.Key)
                || x.DiaDiem.ToLower().Contains(request.Key))
                .ToListAsync();

            return dtqs;
        }

        public async Task XoaDiemThamQuan(int id)
        {
            var dtq = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy điểm tham quan tương ứng");

            _context.DiemThamQuanCTs.Remove(dtq.DiemThamQuanCT);
            _context.DiemThamQuans.Remove(dtq);

            await _context.SaveChangesAsync();
        }
    }
}