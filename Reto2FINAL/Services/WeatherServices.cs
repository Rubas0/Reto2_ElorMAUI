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
                return (null, new List<WeatherDay>());
            }

            if (string.IsNullOrWhiteSpace(municipio))
            {
                return (null, new List<WeatherDay>());
            }

            try
            {
                var queryParts = string.IsNullOrWhiteSpace(territorio)
                    ? municipio
                    : $"{municipio}, {territorio}, Spain";

                var q = Uri.EscapeDataString(queryParts);
                var url = $"{WEATHER_API_URL}?key={API_KEY}&q={q}&days=7&lang=es";

                // Obtener JSON y deserializar
                var json = await _httpClient.GetStringAsync(url);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var response = JsonSerializer.Deserialize<WeatherApiResponse>(json, options);

                if (response == null || response.Current == null)
                {
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
            catch
            {
                // Vacio para gestionarlo en la UI
                return (null, new List<WeatherDay>());
            }
        }
    }
}