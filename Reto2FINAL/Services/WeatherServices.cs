using Reto2FINAL.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reto2FINAL.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "76646781c5924414ac1191646262801";
        private const string WEATHER_API_URL = "https://api.weatherapi.com/v1/forecast.json";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(CurrentWeatherApi?, List<WeatherDay>)> ObtenerClimaAsync(string municipio, string territorio)
        {
            if (string.IsNullOrWhiteSpace(API_KEY))
            {
                Console.WriteLine("[WeatherService] ERROR: API_KEY no configurada.");
                return (null, new List<WeatherDay>());
            }

            if (string.IsNullOrWhiteSpace(municipio))
            {
                Console.WriteLine("[WeatherService] ERROR: municipio vacío.");
                return (null, new List<WeatherDay>());
            }

            try
            {
                // Query que escapa caracteres. (tildes y cosas raras)
                var queryParts = string.IsNullOrWhiteSpace(territorio)
                    ? municipio
                    : $"{municipio}, {territorio}, Spain";

                var query = Uri.EscapeDataString(queryParts);
                var url = $"{WEATHER_API_URL}?key={API_KEY}&q={query}&days=7&lang=es";

                Console.WriteLine($"[WeatherService] Petición a WeatherAPI: {url}");

                // Obtener JSON crudo para poder depurar si algo falla
                var json = await _httpClient.GetStringAsync(url);
                Console.WriteLine($"[WeatherService] Tamaño JSON recibido: {json?.Length ?? 0}");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var response = JsonSerializer.Deserialize<WeatherApiResponse>(json, options);

                if (response == null)
                {
                    Console.WriteLine("[WeatherService] Deserialización falló: response es null. JSON inicio:");
                    Console.WriteLine(json.Length > 2000 ? json.Substring(0, 2000) + "..." : json);
                    return (null, new List<WeatherDay>());
                }

                if (response.Current == null)
                {
                    Console.WriteLine("[WeatherService] response.Current es null. JSON inicio:");
                    Console.WriteLine(json.Length > 2000 ? json.Substring(0, 2000) + "..." : json);
                    return (null, new List<WeatherDay>());
                }

                var previsiones = new List<WeatherDay>();
                if (response.Forecast?.ForecastDay != null)
                {
                    foreach (var day in response.Forecast.ForecastDay)
                    {
                        if (day?.Day != null)
                        {
                            previsiones.Add(new WeatherDay
                            {
                                Date = day.Date,
                                MaxTemp = day.Day.MaxTempC,
                                MinTemp = day.Day.MinTempC,
                                WeatherDescription = day.Day.Condition?.Text ?? "Desconocido"
                            });
                        }
                    }
                }

                return (response.Current, previsiones);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"[WeatherService] HttpRequestException: {httpEx.Message}");
                return (null, new List<WeatherDay>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WeatherService] Exception: {ex}");
                return (null, new List<WeatherDay>());
            }
        }
    }
}