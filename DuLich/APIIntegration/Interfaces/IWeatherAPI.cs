using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;
using ViewModel.ModelsView;

namespace APIIntegration.Interfaces
{
    public interface IWeatherAPI
    {
        [Get("/")]
        Task<WeatherResponse> GetWeather([Query] string lat, [Query] string lon, [Query] string units, [Query] string appid);
    }
}