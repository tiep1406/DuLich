using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DuLichUI.Controllers.Admin
{
    [Route("~/admin/restaurant")]
    [Authorize(Roles = "0")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantAPI _restaurantAPI;

        public RestaurantController(IRestaurantAPI restaurantAPI)
        {
            _restaurantAPI = restaurantAPI;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await _restaurantAPI.GetAll();
            return View("../Admin/Restaurant/Index", restaurants);
        }

        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _restaurantAPI.Toggle(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}