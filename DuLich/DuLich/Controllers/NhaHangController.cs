using DemoCrud.Responsitory;
using DuLich.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaHangController : ControllerBase
    {
        private INhaHangRepositoty _NhaHangRepositoty;

        public NhaHangController(INhaHangRepositoty nhaHangRepositoty)
        {
            _NhaHangRepositoty = nhaHangRepositoty;
        }

        [HttpGet]
        [Route("GetNhaHang")]
        public IActionResult GetNhaHang(int id)
        {
            var ds = _NhaHangRepositoty.GetNhaHang(id);
            return Ok(ds);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var ds = _NhaHangRepositoty.GetAll();
            return Ok(ds);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(NhaHangVM nhaHangVM)
        {
            var ds = _NhaHangRepositoty.Add(nhaHangVM);
            return Ok(ds);
        }

        [HttpPost]
        [Route("datnhahang")]
        public IActionResult datnhahang(DatNhaHangVM nhaHangVM)
        {
            var ds = _NhaHangRepositoty.DatNhaHang(nhaHangVM);
            return Ok(ds);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(NhaHangVM nhaHangVM)
        {
            _NhaHangRepositoty.Update(nhaHangVM);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            _NhaHangRepositoty.Delete(id);
            return Ok();
        }
    }
}