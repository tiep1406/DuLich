using DuLich.Dtos;
using DuLich.Models;
using DuLich.Repository.DBContext;
using DuLich.Request.DiemThamQuan;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DiemThamQuanDto> GetChiTietDiemThamQuan(int id)
        {
            var dtq = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Không tìm thấy điểm tham quan tương ứng");

            return MapToDto(dtq);
        }

        public async Task<List<DiemThamQuanDto>> GetDanhSachDiemThamQuan()
        {
            var dtqs = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .ToListAsync();

            var dtqDtos = new List<DiemThamQuanDto>();
            foreach (var dtq in dtqs)
            {
                dtqDtos.Add(MapToDto(dtq));
            }
            return dtqDtos;
        }

        public async Task ThemDiemThamQuan(ThemDiemThamQuanRequest request)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == request.ChuDichVu)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng tương ứng");
            var dtq = new Models.DiemThamQuan()
            {
                ChuDichVu = request.ChuDichVu,
                DiaDiem = request.DiaDiem,
                Gia = request.Gia,
                TenDiaDiem = request.TenDiaDiem,
                DiemThamQuanCT = new DiemThamQuanCT()
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

        public async Task<List<DiemThamQuanDto>> TimKiemDiemThamQuan(TimKiemDiemThamQuanRequest request)
        {
            var dtqs = await _context.DiemThamQuans
                .Include(x => x.DiemThamQuanCT)
                .Where(x => x.TenDiaDiem.ToLower().Contains(request.Key)
                || x.DiemThamQuanCT.MoTaDichVu.ToLower().Contains(request.Key)
                || x.DiaDiem.ToLower().Contains(request.Key))
                .ToListAsync();

            var dtqDtos = new List<DiemThamQuanDto>();
            foreach (var dtq in dtqs)
            {
                dtqDtos.Add(MapToDto(dtq));
            }
            return dtqDtos;
        }

        private DiemThamQuanDto MapToDto(Models.DiemThamQuan d)
        {
            return new DiemThamQuanDto()
            {
                AnhChiTiet = _uploadService.GetFullPath(d.DiemThamQuanCT.AnhChiTiet),
                AnhDaiDien = _uploadService.GetFullPath(d.AnhDaiDien),
                ChuDichVu = d.ChuDichVu,
                DanhGia = d.DiemThamQuanCT.DanhGia,
                DiaDiem = d.DiaDiem,
                Gia = d.Gia,
                Id = d.Id,
                MoTaDichVu = d.DiemThamQuanCT.MoTaDichVu,
                TenDiaDiem = d.TenDiaDiem
            };
        }
    }
}