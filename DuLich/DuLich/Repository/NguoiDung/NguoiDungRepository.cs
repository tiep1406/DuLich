using DuLich.Repository.DBContext;
using DuLich.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.NguoiDung;

namespace DuLich.Repository.NguoiDung
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;

        public NguoiDungRepository(AppDBContext context, IConfiguration configuration, IUploadService uploadService)
        {
            _context = context;
            _configuration = configuration;
            _uploadService = uploadService;
        }

        public async Task DangKy(DangKyRequest request)
        {
            var nd = await _context.NguoiDungs.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (nd != null)
                throw new InvalidDataException("Email đã tồn tại");
            var nguoiDung = new ViewModel.Models.NguoiDung()
            {
                HoTen = request.HoTen,
                CCCD = request.CCCD,
                Sdt = request.Sdt,
                Email = request.Email,
                GioiTinh = request.GioiTinh,
                NoiO = request.NoiO,
                MatKhau = request.MatKhau,
                PhanQuyen = 3,
                TrangThai = 1,
                AnhDaiDien = "default-avatar.jpg"
            };

            await _context.NguoiDungs.AddAsync(nguoiDung);

            await _context.SaveChangesAsync();
        }

        public async Task<string> CreateJWT(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, nguoiDung.Id.ToString()),
                new Claim(ClaimTypes.Email, nguoiDung.Email),
                new Claim(ClaimTypes.MobilePhone, nguoiDung.Sdt),
                new Claim(ClaimTypes.Role, nguoiDung.PhanQuyen.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponse> DangNhap(DangNhapRequest request)
        {
            var nguoiDung = await _context.NguoiDungs.Where(x => x.Email == request.Email
            && x.MatKhau == request.Password).FirstOrDefaultAsync()
                ?? throw new Exception("Email hoặc mật khẩu không chính xác");
            var token = await CreateJWT(nguoiDung.Id);

            return new AuthResponse() { Token = token };
        }

        public async Task<List<ViewModel.Models.NguoiDung>> GetDanhSachNguoiDung()
        {
            var nguoiDungs = await _context.NguoiDungs.ToListAsync();
            foreach (var item in nguoiDungs)
            {
                item.AnhDaiDien = _uploadService.GetFullPath(item.AnhDaiDien);
            }
            return nguoiDungs;
        }

        public async Task<ViewModel.Models.NguoiDung> GetNguoiDungById(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng");
            nguoiDung.AnhDaiDien = _uploadService.GetFullPath(nguoiDung.AnhDaiDien);
            return nguoiDung;
        }

        public async Task ChinhSuaNguoiDung(ChinhSuaNguoiDungRequest request)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(request.Id)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng");

            nguoiDung.HoTen = request.HoTen;
            nguoiDung.CCCD = request.CCCD;
            nguoiDung.Email = request.Email;
            nguoiDung.GioiTinh = request.GioiTinh;
            if (request.AnhDaiDien != null)
            {
                nguoiDung.AnhDaiDien = await _uploadService.SaveFile(request.AnhDaiDien);
            }
            if (!string.IsNullOrEmpty(request.MatKhau))
                nguoiDung.MatKhau = request.MatKhau;
            nguoiDung.NoiO = request.NoiO;
            nguoiDung.Sdt = request.Sdt;
            nguoiDung.TrangThai = request.TrangThai;
            nguoiDung.PhanQuyen = request.PhanQuyen;

            _context.NguoiDungs.Update(nguoiDung);

            await _context.SaveChangesAsync();
        }

        public async Task Toggle(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng");
            nguoiDung.TrangThai = nguoiDung.TrangThai == 1 ? 0 : 1;
            _context.NguoiDungs.Update(nguoiDung);
            await _context.SaveChangesAsync();
        }
    }
}