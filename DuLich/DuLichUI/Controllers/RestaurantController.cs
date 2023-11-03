using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Models;
using ViewModel.ModelsView;

namespace DuLichUI.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private IRestaurantAPI _restaurantAPI;
        private IUserAPI _userAPI;

        public RestaurantController(IRestaurantAPI restaurantAPI, IUserAPI userAPI)
        {
            _restaurantAPI = restaurantAPI;
            _userAPI = userAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var id = HttpContext.Session.GetString("UserId");
                var restaurants = await _restaurantAPI.GetNhaHangByOwner(int.Parse(id), "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["restaurants"] = restaurants;
                return View();
            }
            catch
            {
                ViewData["restaurants"] = new List<NhaHang>();
                return View();
            }
        }

        public async Task<IActionResult> Create()
        {
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["user"] = x;
            }
            return View(new NhaHangVM());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await _restaurantAPI.GetById(id);
            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> EditNhaHang([FromForm] NhaHangVM request)
        {
            await _restaurantAPI.EditNhaHang(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNhaHang([FromForm] NhaHangVM request)
        {
            if (!ModelState.IsValid)
            {
                var temp = HttpContext.Session.GetString("User");
                if (temp != null)
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                    var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                    ViewData["user"] = x;
                }
                return View("Create", request);
            }
            await _restaurantAPI.CreateNhaHang(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}