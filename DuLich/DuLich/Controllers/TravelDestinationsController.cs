using DuLich.Repository.DiemThamQuan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Request.DiemThamQuan;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "0, 1, 2")]
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

            return Ok(new { statusCode = StatusCodes.Status200OK, data = dtqs });
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemDiemThamQuan([FromQuery] TimKiemDiemThamQuanRequest request)
        {
            var dtqs = await _repository.TimKiemDiemThamQuan(request);

            return Ok(new { statusCode = StatusCodes.Status200OK, data = dtqs });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaDiemThamQuan([FromRoute] int id)
        {
            await _repository.XoaDiemThamQuan(id);

            return Ok(new { statusCode = StatusCodes.Status204NoContent, message = "Xoá điểm tham quan thành công" });
        }

        [HttpPost]
        public async Task<IActionResult> ThemDiemThamQuan([FromForm] ThemDiemThamQuanRequest request)
        {
            await _repository.ThemDiemThamQuan(request);

            return Ok(new { statusCode = StatusCodes.Status201Created, message = "Thêm điểm tham quan du lịch thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> ChinhSuaThongTinDiemThamQuan([FromForm] ChinhSuaDiemThamQuanRequest request)
        {
            await _repository.ChinhSuaDiemThamQuan(request);

            return Ok(new { statusCode = StatusCodes.Status204NoContent, message = "Chỉnh sửa điểm tham quan du lịch thành công" });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietDiemThamQuan([FromRoute] int id)
        {
            var dtq = await _repository.GetChiTietDiemThamQuan(id);

            return Ok(new { statusCode = StatusCodes.Status200OK, data = dtq });
        }
    }
}