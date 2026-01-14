using Reto02_MAUI.Models;

namespace Reto02_MAUI.Services
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(double lat, double lon, string ciudad);
    }
}