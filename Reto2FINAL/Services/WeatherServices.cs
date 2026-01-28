using Reto2FINAL.Models;
using System.Net.Http.Json;

namespace Reto2FINAL.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string OPEN_METEO_URL = "https://api.open-meteo.com/v1/forecast";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener clima actual y previsión 
        public async Task<WeatherData?> ObtenerClimaAsync(double latitud, double longitud)
        {
            try
            {
                var url = $"{OPEN_METEO_URL}?latitude={latitud:F6}&longitude={longitud:F6}" +
                         $"&current=temperature_2m,weather_code,wind_speed_10m" +
                         $"&daily=temperature_2m_max,temperature_2m_min,weather_code" +
                         $"&timezone=Europe/Madrid&forecast_days=7";

                return await _httpClient.GetFromJsonAsync<WeatherData>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener clima: {ex.Message}");
                return null;
            }
        }

        public List<WeatherDay> ProcesarPrevisiones(WeatherData? weatherData)
        {
            var result = new List<WeatherDay>();

            if (weatherData?.Daily == null) return result;

            for (int i = 0; i < weatherData.Daily.Time?.Count; i++)
            {
                result.Add(new WeatherDay
                {
                    Date = weatherData.Daily.Time[i],
                    MaxTemp = weatherData.Daily.Temperature_2m_max?[i] ?? 0,
                    MinTemp = weatherData.Daily.Temperature_2m_min?[i] ?? 0,
                    WeatherCode = weatherData.Daily.Weather_code?[i] ?? 0
                });
            }

            return result;
        }
    }
}