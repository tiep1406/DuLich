using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Models;
using ViewModel.ModelsView;

namespace DuLichUI.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private IHotelAPI _hotelAPI;
        private IUserAPI _userAPI;

        public HotelController(IHotelAPI hotelAPI, IUserAPI userAPI)
        {
            _hotelAPI = hotelAPI;
            _userAPI = userAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var id = HttpContext.Session.GetString("UserId");
                var hotels = await _hotelAPI.GetKhachSanByOwner(int.Parse(id), "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["hotels"] = hotels;
                return View();
            }
            catch
            {
                ViewData["hotels"] = new List<KhachSan>();
                return View();
            }
        }

        public async Task<IActionResult> Booking(int id)
        {
            var tour = await _hotelAPI.GetById(id);
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
        public async Task<IActionResult> CreateBooking([FromForm] DatKhachSanVM request)
        {
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                request.IdNguoiDungs = user.Id;
            }
            await _hotelAPI.DatKhachSan(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/hotel/list?order=true");
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
            return View(new KhachSanVM());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _hotelAPI.GetById(id);
            return View(hotel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var hotel = await _hotelAPI.GetById(id);
            ViewData["khachsans"] = await _hotelAPI.GetAll();
            return View(hotel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var hotels = await _hotelAPI.GetAll();
            return View(hotels);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _hotelAPI.Delete(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditKhachSan([FromForm] KhachSanVM request)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
            await _hotelAPI.EditKhachSan(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateKhachSan([FromForm] KhachSanVM request)
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
            await _hotelAPI.CreateKhachSan(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}