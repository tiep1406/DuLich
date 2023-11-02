using DuLich.Repository.NguoiDung;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Request.NguoiDung;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly INguoiDungRepository _repository;

        public UsersController(INguoiDungRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNguoiDung([FromRoute] int id)
        {
            var nguoiDung = await _repository.GetNguoiDungById(id);

            return Ok(nguoiDung);
        }

        [HttpPut]
        public async Task<IActionResult> SuaThongTinNguoiDung([FromBody] ChinhSuaNguoiDungRequest request)
        {
            await _repository.ChinhSuaNguoiDung(request);

            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "0, 1")]
        public async Task<IActionResult> GetDanhSachNguoiDung()
        {
            var users = await _repository.GetDanhSachNguoiDung();

            return Ok(new { statusCode = StatusCodes.Status200OK, data = users });
        }
    }
}