using Reto02_MAUI.Models;
using System.Text.Json;

namespace Reto02_MAUI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        private const string API_KEY = "17fcd573b2f5b09b5b8540b89b5741d0";
        private const string BASE_URL = "https://api.openweathermap.org/data/2.5";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherInfo> GetWeatherAsync(double lat, double lon, string ciudad)
        {
            if (string.IsNullOrEmpty(API_KEY) || API_KEY == "17fcd573b2f5b09b5b8540b89b5741d0")
            {
                Console.WriteLine("API Key de OpenWeatherMap no configurada");
                return GetMockWeatherInfo(ciudad);
            }

            try
            {
                // Clima actual
                var currentUrl = $"{BASE_URL}/weather?lat={lat}&lon={lon}&appid={API_KEY}&units=metric&lang=es";
                var currentResponse = await _httpClient.GetStringAsync(currentUrl);
                var currentData = JsonSerializer.Deserialize<JsonElement>(currentResponse);

                // Previsión 5 días
                var forecastUrl = $"{BASE_URL}/forecast?lat={lat}&lon={lon}&appid={API_KEY}&units=metric&lang=es";
                var forecastResponse = await _httpClient.GetStringAsync(forecastUrl);
                var forecastData = JsonSerializer.Deserialize<JsonElement>(forecastResponse);

                var weatherInfo = new WeatherInfo
                {
                    Ciudad = ciudad,
                    TemperaturaActual = currentData.GetProperty("main").GetProperty("temp").GetDouble(),
                    Descripcion = currentData.GetProperty("weather")[0].GetProperty("description").GetString(),
                    Icono = currentData.GetProperty("weather")[0].GetProperty("icon").GetString()
                };

                // Procesar previsión (1 dato por día, máximo 5 días)
                var forecastList = forecastData.GetProperty("list");
                var dailyForecasts = new Dictionary<string, WeatherForecast>();

                foreach (var item in forecastList.EnumerateArray())
                {
                    var dt = DateTimeOffset.FromUnixTimeSeconds(item.GetProperty("dt").GetInt64()).DateTime;
                    var dateKey = dt.Date.ToString("yyyy-MM-dd");

                    if (!dailyForecasts.ContainsKey(dateKey) && dailyForecasts.Count < 5)
                    {
                        dailyForecasts[dateKey] = new WeatherForecast
                        {
                            Fecha = dt.Date,
                            TempMax = item.GetProperty("main").GetProperty("temp_max").GetDouble(),
                            TempMin = item.GetProperty("main").GetProperty("temp_min").GetDouble(),
                            Descripcion = item.GetProperty("weather")[0].GetProperty("description").GetString(),
                            Icono = item.GetProperty("weather")[0].GetProperty("icon").GetString()
                        };
                    }
                }

                weatherInfo.Prevision = dailyForecasts.Values.ToList();
                return weatherInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo clima: {ex.Message}");
                return GetMockWeatherInfo(ciudad);
            }
        }

        // Datos de prueba si no hay API Key
        private WeatherInfo GetMockWeatherInfo(string ciudad)
        {
            return new WeatherInfo
            {
                Ciudad = ciudad,
                TemperaturaActual = 15.5,
                Descripcion = "Datos meteorológicos no disponibles (configura API Key)",
                Icono = "02d",
                Prevision = new List<WeatherForecast>
                {
                    new WeatherForecast
                    {
                        Fecha = DateTime.Now.AddDays(1),
                        TempMax = 18,
                        TempMin = 12,
                        Descripcion = "Soleado",
                        Icono = "01d"
                    },
                    new WeatherForecast
                    {
                        Fecha = DateTime.Now.AddDays(2),
                        TempMax = 16,
                        TempMin = 11,
                        Descripcion = "Lluvia",
                        Icono = "10d"
                    },
                    new WeatherForecast
                    {
                        Fecha = DateTime.Now.AddDays(3),
                        TempMax = 17,
                        TempMin = 13,
                        Descripcion = "Nublado",
                        Icono = "03d"
                    }
                }
            };
        }
    }
}