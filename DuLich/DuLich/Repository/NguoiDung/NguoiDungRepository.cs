using DuLich.Dtos;
using DuLich.Repository.DBContext;
using DuLich.Request.NguoiDung;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DuLich.Repository.NguoiDung
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public NguoiDungRepository(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task DangKy(DangKyRequest request)
        {
            var nguoiDung = new Models.NguoiDung()
            {
                CCCD = request.CCCD,
                Sdt = request.Sdt,
                Email = request.Email,
                GioiTinh = request.GioiTinh,
                NoiO = request.NoiO,
                MatKhau = request.MatKhau,
                PhanQuyen = 3,
                TrangThai = 1
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

        public async Task<string> DangNhap(DangNhapRequest request)
        {
            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Email == request.Email
            && x.MatKhau == request.Password)
                ?? throw new Exception("Email hoặc mật khẩu không chính xác");
            var token = await CreateJWT(nguoiDung.Id);

            return token;
        }

        public async Task<List<NguoiDungDto>> GetDanhSachNguoiDung()
        {
            var nguoiDungs = await _context.NguoiDungs.Select(x => new NguoiDungDto()
            {
                CCCD = x.CCCD,
                Email = x.Email,
                GioiTinh = x.GioiTinh,
                Id = x.Id,
                NoiO = x.NoiO,
                Sdt = x.Sdt,
                TrangThai = x.TrangThai,
                PhanQuyen = x.PhanQuyen
            }).ToListAsync();

            return nguoiDungs;
        }

        public async Task<NguoiDungDto> GetNguoiDungById(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng");

            return new NguoiDungDto()
            {
                CCCD = nguoiDung.CCCD,
                Email = nguoiDung.Email,
                GioiTinh = nguoiDung.GioiTinh,
                Id = nguoiDung.Id,
                NoiO = nguoiDung.NoiO,
                Sdt = nguoiDung.Sdt,
                TrangThai = nguoiDung.TrangThai,
                PhanQuyen = nguoiDung.PhanQuyen
            };
        }

        public async Task ChinhSuaNguoiDung(ChinhSuaNguoiDungRequest request)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(request.Id)
                ?? throw new KeyNotFoundException("Không tìm thấy người dùng");

            nguoiDung.CCCD = request.CCCD;
            nguoiDung.Email = request.Email;
            nguoiDung.GioiTinh = request.GioiTinh;
            if (!string.IsNullOrEmpty(request.MatKhau))
                nguoiDung.MatKhau = request.MatKhau;
            nguoiDung.NoiO = request.NoiO;
            nguoiDung.Sdt = request.Sdt;
            nguoiDung.TrangThai = request.TrangThai;
            nguoiDung.PhanQuyen = request.PhanQuyen;

            _context.NguoiDungs.Update(nguoiDung);

            await _context.SaveChangesAsync();
        }
    }
}