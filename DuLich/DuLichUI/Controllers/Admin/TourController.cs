using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DuLichUI.Controllers.Admin
{
    [Route("~/admin/tour")]
    [Authorize(Roles = "0")]
    public class TourController : Controller
    {
        private readonly ITourAPI _tourAPI;

        public TourController(ITourAPI tourAPI)
        {
            _tourAPI = tourAPI;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourAPI.GetAll();
            return View("../Admin/Tour/Index", tours);
        }

        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _tourAPI.Toggle(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}