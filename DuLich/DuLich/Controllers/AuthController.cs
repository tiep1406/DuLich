using DuLich.Models;
using DuLich.Repository.NguoiDung;
using DuLich.Request.NguoiDung;
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
    public class AuthController : ControllerBase
    {
        private readonly INguoiDungRepository _repository;

        public AuthController(INguoiDungRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> DangKy([FromBody] DangKyRequest request)
        {
            await _repository.DangKy(request);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> DangNhap([FromBody] DangNhapRequest request)
        {
            var token = await _repository.DangNhap(request);

            return Ok(new { token });
        }
    }
}