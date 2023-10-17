using DuLich.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhSachNguoiDungController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Tat ca du lieu nguoi dung";
        }
        [HttpGet]
        [Route("Details")]
        public string Details()
        {
            return "Du lieu chi tiet";
        }

        [Authorize]
        [HttpPost]
        public string ThemNguoiDung(NguoiDung nguoiDung)
        {
            return "Them nguoi dung" + nguoiDung.Email;
        }
    }
}
