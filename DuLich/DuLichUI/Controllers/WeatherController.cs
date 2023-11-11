using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace DuLichUI.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherAPI _weatherAPI;

        public WeatherController()
        {
            _weatherAPI = RestClient.For<IWeatherAPI>("https://openweathermap.org/data/2.5/onecall");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string coord)
        {
            var temp = coord.Split("-");
            var resp = await _weatherAPI.GetWeather(temp[0], temp[1], "metric", "439d4b804bc8187953eb36d2a8c26a02");
            return Ok(resp);
        }
    }
}