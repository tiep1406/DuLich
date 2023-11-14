using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

                var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["user"] = x;
            }
            return View();
        }

        public async Task<IActionResult> GuestOrder()
        {
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["user"] = x;
                var tours = (await _tourAPI.GetTourByOwner(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken")));
                var orderTours = new List<DatTour>();
                tours.ForEach(x =>
                {
                    x.DatTours.ForEach(y =>
                    {
                        y.Tour = x;
                    });
                    orderTours.AddRange(x.DatTours);
                });
                ViewData["tours"] = orderTours;

                var hotels = (await _hotelAPI.GetKhachSanByOwner(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken")));
                var orderHotels = new List<DatKhachSan>();
                hotels.ForEach(x =>
                {
                    x.DatKhachSans.ForEach(y =>
                    {
                        y.KhachSan = x;
                    });
                    orderHotels.AddRange(x.DatKhachSans);
                });
                ViewData["hotels"] = orderHotels;

                var restaurants = (await _restaurantAPI.GetNhaHangByOwner(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken")));
                var orderRestaurants = new List<DatNhaHang>();
                restaurants.ForEach(x =>
                {
                    x.DatNhaHangs.ForEach(y =>
                    {
                        y.NhaHang = x;
                    });
                    orderRestaurants.AddRange(x.DatNhaHangs);
                });
                ViewData["restaurants"] = orderRestaurants;

                var deliverys = (await _deliveryAPI.GetVanChuyenByOwner(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken")));
                var orderDeliverys = new List<DatVanChuyen>();
                deliverys.ForEach(x =>
                {
                    x.DatVanChuyens.ForEach(y =>
                    {
                        y.VanChuyen = x;
                    });
                    orderDeliverys.AddRange(x.DatVanChuyens);
                });
                ViewData["deliverys"] = orderDeliverys;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm] ChinhSuaNguoiDungRequest request)
        {
            await _userAPI.EditUser(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/user");
        }
    }
}