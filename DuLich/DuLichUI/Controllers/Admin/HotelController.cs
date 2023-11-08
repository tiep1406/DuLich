using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DuLichUI.Controllers.Admin
{
    [Route("~/admin/hotel")]
    [Authorize(Roles = "0")]
    public class HotelController : Controller
    {
        private readonly IHotelAPI _hotelAPI;

        public HotelController(IHotelAPI hotelAPI)
        {
            _hotelAPI = hotelAPI;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelAPI.GetAll();
            return View("../Admin/Hotel/Index", hotels);
        }

        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _hotelAPI.Toggle(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}