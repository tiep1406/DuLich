using DemoCrud.Responsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ModelsView;
using ViewModel.Request.KhachSan;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KhachSanController : ControllerBase
    {
        private readonly IKhachSanRepository _repository;

        public KhachSanController(IKhachSanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDanhSachKhachSan()
        {
            var dtqs = await _repository.GetAll();

            return Ok(dtqs);
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetDanhSachKhachSanByChuDichVu([FromRoute] int id)
        {
            var dtqs = await _repository.GetByOwner(id);

            return Ok(dtqs);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> TimKiemKhachSan([FromBody] TimKiemKhachSanRequest request)
        {
            var dtqs = await _repository.Search(request);

            return Ok(dtqs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaKhachSan([FromRoute] int id)
        {
            await _repository.Delete(id);

            return NoContent();
        }

        [HttpPut("toggle/{id}")]
        public async Task<IActionResult> Toggle([FromRoute] int id)
        {
            await _repository.Toggle(id);

            return NoContent();
        }

        [HttpPost("order")]
        public async Task<IActionResult> DatKhachSan([FromBody] DatKhachSanVM request)
        {
            await _repository.DatKhachSan(request);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ThemKhachSan([FromForm] KhachSanVM request)
        {
            await _repository.Add(request);

            return NoContent();
        }

        [HttpPost("binhluan")]
        public async Task<IActionResult> BinhLuanKhachSan(BinhLuanKhachSanVM request)
        {
            await _repository.BinhLuan(request);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChinhSuaThongTinKhachSan([FromForm] KhachSanVM request)
        {
            await _repository.Update(request);

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChiTietKhachSan([FromRoute] int id)
        {
            var dtq = await _repository.GetKhachSan(id);

            return Ok(dtq);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetDanhSachKhachSanByNguoiDung([FromRoute] int id)
        {
            var tours = await _repository.GetKhachSanByNguoiDung(id);

            return Ok(tours);
        }
    }
}