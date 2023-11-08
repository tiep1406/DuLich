using DemoCrud.Responsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ModelsView;
using ViewModel.Request.VanChuyen;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VanChuyenController : ControllerBase
    {
        private readonly IVanChuyenRepository _repository;

        public VanChuyenController(IVanChuyenRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDanhSachVanChuyen()
        {
            var dtqs = await _repository.GetAll();

            return Ok(dtqs);
        }

        [HttpPut("toggle/{id}")]
        public async Task<IActionResult> Toggle([FromRoute] int id)
        {
            await _repository.Toggle(id);

            return NoContent();
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetDanhSachVanChuyenByChuDichVu([FromRoute] int id)
        {
            var dtqs = await _repository.GetByOwner(id);

            return Ok(dtqs);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemVanChuyen([FromBody] TimKiemVanChuyenRequest request)
        {
            var dtqs = await _repository.Search(request);

            return Ok(dtqs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaVanChuyen([FromRoute] int id)
        {
            await _repository.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ThemVanChuyen([FromForm] VanChuyenVM request)
        {
            await _repository.Add(request);

            return NoContent();
        }

        [HttpPost("order")]
        public async Task<IActionResult> DatVanChuyen([FromBody] DatVanChuyenVM request)
        {
            await _repository.DatVanChuyen(request);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChinhSuaThongTinVanChuyen([FromForm] VanChuyenVM request)
        {
            await _repository.Update(request);

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietVanChuyen([FromRoute] int id)
        {
            var dtq = await _repository.GetVanChuyen(id);

            return Ok(dtq);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetDanhSachVanChuyenByNguoiDung([FromRoute] int id)
        {
            var tours = await _repository.GetVanChuyenByNguoiDung(id);

            return Ok(tours);
        }
    }
}