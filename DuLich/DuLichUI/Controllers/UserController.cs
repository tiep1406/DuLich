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

        public UserController(IUserAPI userAPI)
        {
            _userAPI = userAPI;
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

        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm] ChinhSuaNguoiDungRequest request)
        {
            await _userAPI.EditUser(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}