using DemoCrud.Responsitory;
using DuLich.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachSanController : ControllerBase
    {
        private IKhachSanRepositoty _KhachSanRepositoty;

        public KhachSanController(IKhachSanRepositoty KhachSanRepositoty)
        {
            _KhachSanRepositoty = KhachSanRepositoty;
        }

        [HttpGet]
        [Route("GetKhachSan/{id}")]
        public IActionResult GetKhachSan(int id)
        {
            var ds = _KhachSanRepositoty.GetKhachSan(id);
            return Ok(ds);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var ds = _KhachSanRepositoty.GetAll();
            return Ok(ds);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(KhachSanVM KhachSanVM)
        {
            var ds = _KhachSanRepositoty.Add(KhachSanVM);
            return Ok(ds);
        }

        [HttpPost]
        [Route("datKhachSan")]
        public IActionResult datKhachSan(DatKhachSanVM KhachSanVM)
        {
            var ds = _KhachSanRepositoty.DatKhachSan(KhachSanVM);
            return Ok(ds);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(KhachSanVM KhachSanVM)
        {
            _KhachSanRepositoty.Update(KhachSanVM);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            _KhachSanRepositoty.Delete(id);
            return Ok();
        }
    }
}