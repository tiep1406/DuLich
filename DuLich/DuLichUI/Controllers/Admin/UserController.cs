using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuLichUI.Controllers.Admin
{
    [Route("~/admin/user")]
    [Route("~/admin")]
    [Authorize(Roles = "0")]
    public class UserController : Controller
    {
        private readonly IUserAPI _userAPI;

        public UserController(IUserAPI userAPI)
        {
            _userAPI = userAPI;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userAPI.GetAll("Bearer " + HttpContext.Session.GetString("BearerToken"));
            return View("../Admin/User/Index", users);
        }

        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle([FromRoute] int id)
        {
            await _userAPI.Toggle(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return Redirect("/admin/user");
        }
    }
}