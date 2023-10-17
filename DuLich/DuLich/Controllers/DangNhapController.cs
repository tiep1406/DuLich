using DuLich.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangNhapController : ControllerBase
    {
        private IConfiguration _configuration;
        public DangNhapController(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        private NguoiDung AuthenticateUser(NguoiDung nguoiDung)
        {
            NguoiDung _nguoiDung = new NguoiDung();
            if (nguoiDung.Email == "admin@gmail.com" && nguoiDung.MatKhau == "123456")
            {
                _nguoiDung = new NguoiDung { Email = nguoiDung.Email };
            }
            return _nguoiDung;
        }

        private string GenerateToken(NguoiDung nguoiDung)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult DangNhap(NguoiDung nguoiDung)
        {
            IActionResult response = Unauthorized();
            var nguoiDung_ = AuthenticateUser(nguoiDung);
            if (nguoiDung_ != null) { 
                var token = GenerateToken(nguoiDung_);
                response = Ok(new {token = token});
            }
            return response;
        }
    }
}
