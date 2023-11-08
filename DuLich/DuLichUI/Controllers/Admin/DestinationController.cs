using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DuLichUI.Controllers.Admin
{
    [Route("~/admin/destination")]
    [Authorize(Roles = "0")]
    public class DestinationController : Controller
    {
        private readonly IDestinationAPI _destinationAPI;

        public DestinationController(IDestinationAPI destinationAPI)
        {
            _destinationAPI = destinationAPI;
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _destinationAPI.GetAll();
            return View("../Admin/Destination/Index", destinations);
        }

        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _destinationAPI.Toggle(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}