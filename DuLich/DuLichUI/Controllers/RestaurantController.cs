using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.NhaHang;
using ViewModel.Request.Tour;

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

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var hotel = await _restaurantAPI.GetById(id);
            ViewData["nhahangs"] = await _restaurantAPI.GetAll();
            return View(hotel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var hotels = await _restaurantAPI.GetAll();
            return View(hotels);
        }

        public async Task<IActionResult> Booking(int id)
        {
            var tour = await _restaurantAPI.GetById(id);
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["user"] = x;
            }
            return View(tour);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] DatNhaHangVM request)
        {
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                request.IdNguoiDungs = user.Id;
            }
            await _restaurantAPI.DatNhaHang(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/restaurant/list?order=true");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] TimKiemNhaHangRequest request)
        {
            var restaurants = await _restaurantAPI.SearchNhaHang(request);
            return View("List", restaurants);
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
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
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