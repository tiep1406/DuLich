using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestEase.Implementation;
using System.Linq.Expressions;
using ViewModel.Models;
using ViewModel.ModelsView;
using ViewModel.Request.Tour;

namespace DuLichUI.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        private ITourAPI _tourAPI;
        private IUserAPI _userAPI;

        public TourController(ITourAPI tourAPI, IUserAPI userAPI)
        {
            _tourAPI = tourAPI;
            _userAPI = userAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var id = HttpContext.Session.GetString("UserId");
                var tours = await _tourAPI.GetTourByOwner(int.Parse(id), "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["tours"] = tours;
                var temp = HttpContext.Session.GetString("User");
                if (temp != null)
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                    var x = await _userAPI.GetById(user.Id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
                    ViewData["user"] = x;
                }
                return View();
            }
            catch
            {
                ViewData["tours"] = new List<Tour>();
                return View();
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var tour = await _tourAPI.GetById(id);
            if (!tour.TrangThai)
            {
                return Redirect("/tour/list");
            }
            ViewData["tours"] = await _tourAPI.GetAll();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                ViewData["UserId"] = HttpContext.Session.GetString("UserId");
            return View(tour);
        }

        public async Task<IActionResult> Booking(int id)
        {
            var tour = await _tourAPI.GetById(id);
            if (!tour.TrangThai)
            {
                return Redirect("/tour/list");
            }
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
        public async Task<IActionResult> CreateBooking([FromForm] DatTourRequest request)
        {
            var tour = await _tourAPI.GetById(request.IdTour);
            if (!tour.TrangThai)
            {
                return Redirect("/tour/list?order=false");
            }
            var temp = HttpContext.Session.GetString("User");
            if (temp != null)
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<NguoiDung>(temp);
                request.IdNguoiDung = user.Id;
            }
            await _tourAPI.DatTour(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));
            return Redirect("/tour/list?order=true");
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var tours = await _tourAPI.GetAll();
            return View(tours);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] TimKiemTourRequest request)
        {
            var tours = await _tourAPI.SearchTour(request);
            return View("List", tours);
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
            return View(new ThemTourRequest());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _tourAPI.GetById(id);
            return View(tour);
        }

        [HttpPost]
        public async Task<IActionResult> EditTour([FromForm] ChinhSuaTourRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
            await _tourAPI.EditTour(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/tour");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour([FromForm] ThemTourRequest request)
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
            await _tourAPI.CreateTour(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/tour");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBinhLuan([FromForm] BinhLuanTourVM request)
        {
            await _tourAPI.BinhLuanTour(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/tour/detail/" + request.IdTour);
        }
    }
}