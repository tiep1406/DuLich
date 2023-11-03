﻿using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Models;
using ViewModel.Request.DiemThamQuan;

namespace DuLichUI.Controllers
{
    [Authorize]
    public class DestinationController : Controller
    {
        private IDestinationAPI _destinationAPI;
        private IUserAPI _userAPI;

        public DestinationController(IDestinationAPI destinationAPI, IUserAPI userAPI)
        {
            _destinationAPI = destinationAPI;
            _userAPI = userAPI;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var id = HttpContext.Session.GetString("UserId");
                var destinations = await _destinationAPI.GetDiemThamQuanByOwner(int.Parse(id), "Bearer " + HttpContext.Session.GetString("BearerToken"));
                ViewData["destinations"] = destinations;
                return View();
            }
            catch
            {
                ViewData["destinations"] = new List<DiemThamQuan>();
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
            return View(new ThemDiemThamQuanRequest());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var destination = await _destinationAPI.GetById(id);
            return View(destination);
        }

        [HttpPost]
        public async Task<IActionResult> EditDiemThamQuan([FromForm] ChinhSuaDiemThamQuanRequest request)
        {
            await _destinationAPI.EditDiemThamQuan(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiemThamQuan([FromForm] ThemDiemThamQuanRequest request)
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
            await _destinationAPI.CreateDiemThamQuan(request, "Bearer " + HttpContext.Session.GetString("BearerToken"));

            return RedirectToAction("Index");
        }
    }
}