using DuLich.Repository.Tour;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Request.Tour;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToursController : ControllerBase
    {
        private readonly ITourRepository _repository;

        public ToursController(ITourRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDanhSachTour()
        {
            var tours = await _repository.GetDanhSachTour();

            return Ok(new { statusCode = StatusCodes.Status200OK, data = tours });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietTour([FromRoute] int id)
        {
            var tour = await _repository.GetChiTietTour(id);

            return Ok(new { statusCode = StatusCodes.Status200OK, data = tour });
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemTour([FromQuery] TimKiemTourRequest request)
        {
            var tours = await _repository.TimKiemTour(request);

            return Ok(new { statusCode = StatusCodes.Status200OK, data = tours });
        }

        [HttpGet("user/{id}")]
        [Authorize(Roles = "0, 1, 2, 3")]
        public async Task<IActionResult> GetDanhSachTourByNguoiDung([FromRoute] int id)
        {
            var tours = await _repository.GetDanhSachTourByNguoiDung(id);

            return Ok(new { statusCode = StatusCodes.Status200OK, data = tours });
        }

        [HttpGet("owner/{id}")]
        [Authorize(Roles = "0, 1, 2")]
        public async Task<IActionResult> GetDanhSachTourByChuDichVu([FromRoute] int id)
        {
            var tours = await _repository.GetDanhSachTourByChuDichVu(id);

            return Ok(new { statusCode = StatusCodes.Status200OK, data = tours });
        }

        [HttpPost]
        [Authorize(Roles = "0, 1, 2")]
        public async Task<IActionResult> ThemTour([FromForm] ThemTourRequest request)
        {
            await _repository.ThemTour(request);

            return Ok(new { statusCode = StatusCodes.Status201Created, message = "Thêm tour thành công" });
        }

        [HttpPut]
        [Authorize(Roles = "0, 1, 2")]
        public async Task<IActionResult> ChinhSuaThongTinTour([FromForm] ChinhSuaTourRequest request)
        {
            await _repository.ChinhSuaTour(request);

            return Ok(new { statusCode = StatusCodes.Status204NoContent, message = "Chỉnh sửa tour thành công" });
        }

        [HttpPost("order")]
        [Authorize(Roles = "0, 1, 2, 3")]
        public async Task<IActionResult> DatTour([FromBody] DatTourRequest request)
        {
            await _repository.DatTour(request);

            return Ok(new { statusCode = StatusCodes.Status201Created, message = "Đặt tour thành công" });
        }
    }
}