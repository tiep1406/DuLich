using DemoCrud.Responsitory;
using DuLich.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VanChuyenController : ControllerBase
    {
        private IVanChuyenRepositoty _VanChuyenRepositoty;

        public VanChuyenController(IVanChuyenRepositoty VanChuyenRepositoty)
        {
            _VanChuyenRepositoty = VanChuyenRepositoty;
        }

        [HttpGet]
        [Route("GetVanChuyen/{id}")]
        public IActionResult GetVanChuyen(int id)
        {
            var ds = _VanChuyenRepositoty.GetVanChuyen(id);
            return Ok(ds);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var ds = _VanChuyenRepositoty.GetAll();
            return Ok(ds);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(VanChuyenVM VanChuyenVM)
        {
            var ds = _VanChuyenRepositoty.Add(VanChuyenVM);
            return Ok(ds);
        }

        [HttpPost]
        [Route("datVanChuyen")]
        public IActionResult datVanChuyen(DatVanChuyenVM VanChuyenVM)
        {
            var ds = _VanChuyenRepositoty.DatVanChuyen(VanChuyenVM);
            return Ok(ds);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(VanChuyenVM VanChuyenVM)
        {
            _VanChuyenRepositoty.Update(VanChuyenVM);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            _VanChuyenRepositoty.Delete(id);
            return Ok();
        }
    }
}