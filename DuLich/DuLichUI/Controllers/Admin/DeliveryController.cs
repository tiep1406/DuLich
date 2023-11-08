using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DuLichUI.Controllers.Admin
{
    [Route("~/admin/delivery")]
    [Authorize(Roles = "0")]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryAPI _deliveryAPI;

        public DeliveryController(IDeliveryAPI deliveryAPI)
        {
            _deliveryAPI = deliveryAPI;
        }

        public async Task<IActionResult> Index()
        {
            var deliverys = await _deliveryAPI.GetAll();
            return View("../Admin/Delivery/Index", deliverys);
        }

        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _deliveryAPI.Toggle(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}