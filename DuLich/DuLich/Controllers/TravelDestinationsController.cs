using DuLich.Repository.DiemThamQuan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Request.DiemThamQuan;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TravelDestinationsController : ControllerBase
    {
        private readonly IDiemThamQuanRepository _repository;

        public TravelDestinationsController(IDiemThamQuanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDanhSachDiemThamQuan()
        {
            var dtqs = await _repository.GetDanhSachDiemThamQuan();

            return Ok(dtqs);
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetDanhSachDiemThamQuanByChuDichVu([FromRoute] int id)
        {
            var dtqs = await _repository.GetDanhSachDiemThamQuanByChuDichVu(id);

            return Ok(dtqs);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemDiemThamQuan([FromQuery] TimKiemDiemThamQuanRequest request)
        {
            var dtqs = await _repository.TimKiemDiemThamQuan(request);

            return Ok(dtqs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaDiemThamQuan([FromRoute] int id)
        {
            await _repository.XoaDiemThamQuan(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ThemDiemThamQuan([FromForm] ThemDiemThamQuanRequest request)
        {
            await _repository.ThemDiemThamQuan(request);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChinhSuaThongTinDiemThamQuan([FromForm] ChinhSuaDiemThamQuanRequest request)
        {
            await _repository.ChinhSuaDiemThamQuan(request);

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietDiemThamQuan([FromRoute] int id)
        {
            var dtq = await _repository.GetChiTietDiemThamQuan(id);

            return Ok(dtq);
        }
    }
}