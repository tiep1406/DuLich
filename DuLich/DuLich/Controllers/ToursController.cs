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

            return Ok(tours);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietTour([FromRoute] int id)
        {
            var tour = await _repository.GetChiTietTour(id);

            return Ok(tour);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemTour([FromQuery] TimKiemTourRequest request)
        {
            var tours = await _repository.TimKiemTour(request);

            return Ok(tours);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetDanhSachTourByNguoiDung([FromRoute] int id)
        {
            var tours = await _repository.GetDanhSachTourByNguoiDung(id);

            return Ok(tours);
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetDanhSachTourByChuDichVu([FromRoute] int id)
        {
            var tours = await _repository.GetDanhSachTourByChuDichVu(id);

            return Ok(tours);
        }

        [HttpPost]
        public async Task<IActionResult> ThemTour([FromForm] ThemTourRequest request)
        {
            await _repository.ThemTour(request);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChinhSuaThongTinTour([FromForm] ChinhSuaTourRequest request)
        {
            await _repository.ChinhSuaTour(request);

            return NoContent();
        }

        [HttpPost("order")]
        public async Task<IActionResult> DatTour([FromBody] DatTourRequest request)
        {
            await _repository.DatTour(request);

            return NoContent();
        }
    }
}