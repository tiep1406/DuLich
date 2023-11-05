using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using ViewModel.Models;
using ViewModel.Request.NguoiDung;

namespace DuLichUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserAPI _userAPI;
        private readonly ITourAPI _tourAPI;
        private readonly IDeliveryAPI _deliveryAPI;
        private readonly IHotelAPI _hotelAPI;
        private readonly IRestaurantAPI _restaurantAPI;

        public UserController(IUserAPI userAPI, ITourAPI tourAPI, IDeliveryAPI deliveryAPI, IHotelAPI hotelAPI, IRestaurantAPI restaurantAPI)
        {
            _userAPI = userAPI;
            _tourAPI = tourAPI;
            _deliveryAPI = deliveryAPI;
            _hotelAPI = hotelAPI;
            _restaurantAPI = restaurantAPI;
        }

        public async Task<IActionResult> Index()
        {
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["user"] = x;
            }
            return View();
        }

        public async Task<IActionResult> Order()
        {
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                ViewData["tours"] = await _tourAPI.GetTourByUser(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["hotels"] = await _hotelAPI.GetKhachSanOrder(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["restaurants"] = await _restaurantAPI.GetNhaHangOrder(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["deliverys"] = await _deliveryAPI.GetVanChuyenOrder(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm] ChinhSuaNguoiDungRequest request)
        {
            await _userAPI.EditUser(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}