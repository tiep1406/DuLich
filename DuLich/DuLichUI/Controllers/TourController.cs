using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestEase.Implementation;
using System.Linq.Expressions;
using ViewModel.Models;
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
                return View();
            }
            catch
            {
                ViewData["tours"] = new List<Tour>();
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
            await _tourAPI.EditTour(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}