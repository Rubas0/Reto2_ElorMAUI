using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reto02_MAUI.Models;
using System.Text.Json;

namespace Reto02_MAUI.Services
{
    /// <summary>
    /// Servicio meteorológico usando Geoapify Weather API
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        // IMPORTANTE: Reemplazar con tu API Key de Geoapify
        // Obtener gratis en: https://www.geoapify.com/
        private const string API_KEY = "e6869cff205c460f96d083c00991d203";
        private const string BASE_URL = "https://api.geoapify.com/v1/weather";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtener clima actual y previsión por coordenadas usando Geoapify
        /// </summary>
        public async Task<WeatherInfo> GetWeatherByCoordinatesAsync(string latitud, string longitud)
        {
            try
            {
                // Convertir coordenadas a formato numérico
                var lat = double.Parse(latitud.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                var lon = double.Parse(longitud.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                // URL de Geoapify Weather API
                // Documentación: https://apidocs.geoapify.com/docs/weather-api/#weather
                var url = $"{BASE_URL}?lat={lat}&lon={lon}&apiKey={API_KEY}&format=json&lang=es";

                var response = await _httpClient.GetStringAsync(url);
                var weatherData = JsonSerializer.Deserialize<JsonElement>(response);

                // Parsear respuesta de Geoapify
                var current = weatherData.GetProperty("current");
                var forecast = weatherData.GetProperty("forecast");

                var weather = new WeatherInfo
                {
                    Temperatura = current.GetProperty("temperature").GetDouble(),
                    Descripcion = current.GetProperty("summary").GetString(),
                    Icono = current.GetProperty("icon").GetString(),
                    Humedad = current.GetProperty("humidity").GetInt32(),
                    VelocidadViento = current.GetProperty("wind").GetProperty("speed").GetDouble(),
                    Prevision = ParsePrevisionGeoapify(forecast)
                };

                Console.WriteLine($"[GEOAPIFY] Obtenido clima para lat:{lat}, lon:{lon} - {weather.Temperatura}°C");
                return weather;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo clima de Geoapify: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Parsear previsión de Geoapify para los próximos 5 días
        /// </summary>
        private List<PrevisionDia> ParsePrevisionGeoapify(JsonElement forecastData)
        {
            var prevision = new List<PrevisionDia>();

            try
            {
                var dailyForecast = forecastData.GetProperty("daily");

                // Tomar los primeros 5 días
                int count = 0;
                foreach (var day in dailyForecast.EnumerateArray())
                {
                    if (count >= 5) break;

                    var fecha = DateTimeOffset.FromUnixTimeSeconds(day.GetProperty("time").GetInt64()).DateTime;

                    prevision.Add(new PrevisionDia
                    {
                        Fecha = fecha,
                        TempMax = day.GetProperty("temperature").GetProperty("max").GetDouble(),
                        TempMin = day.GetProperty("temperature").GetProperty("min").GetDouble(),
                        Descripcion = day.GetProperty("summary").GetString(),
                        Icono = day.GetProperty("icon").GetString()
                    });

                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parseando previsión de Geoapify: {ex.Message}");
            }

            return prevision;
        }
    }
}