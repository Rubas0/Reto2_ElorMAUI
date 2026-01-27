using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Reto2FINAL.Models;

namespace Reto2FINAL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.open-meteo.com/v1/forecast";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse?> ObtenerClimaPorCoordendasAsync(float latitud, float longitud)
        {
            try
            {
                // IMPORTANTE: Open-Meteo requiere formato americano (punto decimal)
                var culturaAmericana = new CultureInfo("en-US");
                var latStr = latitud.ToString(culturaAmericana);
                var lonStr = longitud.ToString(culturaAmericana);

                var url = $"{BaseUrl}?latitude={latStr}&longitude={lonStr}" +
                    $"&current=temperature_2m,relative_humidity_2m,is_day" +
                    $"&hourly=temperature_2m" +
                    $"&daily=weather_code,temperature_2m_max,temperature_2m_min,uv_index_max" +
                    $"&timezone=auto";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<WeatherResponse>(json, opciones);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error obteniendo clima: {ex.Message}");
                return null;
            }
        }
    }
}
