using DemoCrud.Responsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ModelsView;
using ViewModel.Request.NhaHang;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NhaHangController : ControllerBase
    {
        private readonly INhaHangRepository _repository;

        public NhaHangController(INhaHangRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDanhSachNhaHang()
        {
            var dtqs = await _repository.GetAll();

            return Ok(dtqs);
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetDanhSachNhaHangByChuDichVu([FromRoute] int id)
        {
            var dtqs = await _repository.GetByOwner(id);

            return Ok(dtqs);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemNhaHang([FromQuery] TimKiemNhaHangRequest request)
        {
            var dtqs = await _repository.Search(request);

            return Ok(dtqs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaNhaHang([FromRoute] int id)
        {
            await _repository.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ThemNhaHang([FromForm] NhaHangVM request)
        {
            await _repository.Add(request);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChinhSuaThongTinNhaHang([FromForm] NhaHangVM request)
        {
            await _repository.Update(request);

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietNhaHang([FromRoute] int id)
        {
            var dtq = await _repository.GetNhaHang(id);

            return Ok(dtq);
        }
    }
}