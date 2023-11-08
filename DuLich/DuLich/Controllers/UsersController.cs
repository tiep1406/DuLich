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

        [HttpPut("toggle/{id}")]
        public async Task<IActionResult> Toggle([FromRoute] int id)
        {
            await _repository.Toggle(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> SuaThongTinNguoiDung([FromForm] ChinhSuaNguoiDungRequest request)
        {
            await _repository.ChinhSuaNguoiDung(request);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetDanhSachNguoiDung()
        {
            var users = await _repository.GetDanhSachNguoiDung();

            return Ok(users);
        }
    }
}