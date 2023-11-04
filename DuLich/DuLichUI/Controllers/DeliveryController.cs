using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ViewModel.Models;
using ViewModel.ModelsView;

namespace DuLichUI.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private IDeliveryAPI _deliveryAPI;
        private IUserAPI _userAPI;

        public DeliveryController(IDeliveryAPI deliveryAPI, IUserAPI userAPI)
        {
            _deliveryAPI = deliveryAPI;
            _userAPI = userAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var id = HttpContext.Session.GetString("UserId");
                var deliverys = await _deliveryAPI.GetVanChuyenByOwner(int.Parse(id), "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["deliverys"] = deliverys;
                return View();
            }
            catch
            {
                ViewData["deliverys"] = new List<VanChuyen>();
                return View();
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var deliverys = await _deliveryAPI.GetAll();
            return View(deliverys);
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
            return View(new VanChuyenVM());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var delivery = await _deliveryAPI.GetById(id);
            return View(delivery);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var delivery = await _deliveryAPI.Delete(id, "Bearer " + HttpContext.Session.GetString("BearerToken"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditVanChuyen([FromForm] VanChuyenVM request)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
            await _deliveryAPI.EditVanChuyen(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateVanChuyen([FromForm] VanChuyenVM request)
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
            await _deliveryAPI.CreateVanChuyen(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}