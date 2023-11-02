using DuLich.Repository.NguoiDung;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ModelsView;
using ViewModel.Request.NguoiDung;

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
            var authResp = await _repository.DangNhap(request);

            return Ok(authResp);
        }
    }
}