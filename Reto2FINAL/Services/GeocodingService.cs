using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Reto2FINAL.Models;

namespace Reto2FINAL.Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "e6869cff205c460f96d083c00991d203";
        private const string BaseUrl = "https://api.geoapify.com/v1/geocode/search";

        public GeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeocodingResponse?> BuscarCiudadAsync(string nombreCiudad)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreCiudad))
                    return null;

                var url = $"{BaseUrl}?text={Uri.EscapeDataString(nombreCiudad)}&format=json&apiKey={ApiKey}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<GeocodingResponse>(json, opciones);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error buscando ciudad: {ex.Message}");
                return null;
            }
        }
    }
}
