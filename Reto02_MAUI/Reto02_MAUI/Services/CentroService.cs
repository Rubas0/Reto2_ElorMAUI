using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reto02_MAUI.Models;
using System.Text.Json;

namespace Reto02_MAUI.Services
{
    public class CentroService : ICentroService
    {
        private List<Centro> _centros;
        private readonly HttpClient _httpClient;

        // CONFIGURACION:  Cambiar cuando ElorBase esté desplegado
        private const bool USE_API_REST = false; // true cuando Spring Boot esté listo
        private const string API_BASE_URL = "http://localhost:8080/api/centros";

        public CentroService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Centro>> GetAllCentrosAsync()
        {
            if (_centros != null) return _centros;

            if (USE_API_REST)
            {
                return await GetAllCentrosFromApiAsync();
            }
            else
            {
                return await GetAllCentrosFromLocalJsonAsync();
            }
        }

        // OPCION A: API REST (cuando ElorBase esté listo)
        private async Task<List<Centro>> GetAllCentrosFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{API_BASE_URL}");
                _centros = JsonSerializer.Deserialize<List<Centro>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                }) ?? new List<Centro>();

                Console.WriteLine($"[API REST] Cargados {_centros.Count} centros");
                return _centros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error API REST: {ex.Message}");
                // Fallback a JSON local
                return await GetAllCentrosFromLocalJsonAsync();
            }
        }

        // OPCION B: JSON Local (desarrollo sin backend)
        private async Task<List<Centro>> GetAllCentrosFromLocalJsonAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("EuskadiLatLon.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                _centros = JsonSerializer.Deserialize<List<Centro>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                }) ?? new List<Centro>();

                Console.WriteLine($"[JSON LOCAL] Cargados {_centros.Count} centros");
                return _centros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando JSON local: {ex.Message}");
                _centros = new List<Centro>();
                return _centros;
            }
        }

        // USO DE LINQ (requisito de la rúbrica)
        public async Task<List<Centro>> GetCentrosFiltradosAsync(CentroFilter filter)
        {
            if (USE_API_REST)
            {
                // Delegar filtrado al servidor
                return await GetCentrosFiltradosFromApiAsync(filter);
            }
            else
            {
                // Filtrado local con LINQ
                return await GetCentrosFiltradosLocalAsync(filter);
            }
        }

        // Filtrado mediante API REST
        private async Task<List<Centro>> GetCentrosFiltradosFromApiAsync(CentroFilter filter)
        {
            try
            {
                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(filter.DTITUC))
                    queryParams.Add($"dtituc={Uri.EscapeDataString(filter.DTITUC)}");
                if (!string.IsNullOrEmpty(filter.DTERRE))
                    queryParams.Add($"dterre={Uri.EscapeDataString(filter.DTERRE)}");
                if (!string.IsNullOrEmpty(filter.DMUNIC))
                    queryParams.Add($"dmunic={Uri.EscapeDataString(filter.DMUNIC)}");

                var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var url = $"{API_BASE_URL}/filtrados{query}";

                var response = await _httpClient.GetStringAsync(url);
                var centros = JsonSerializer.Deserialize<List<Centro>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                }) ?? new List<Centro>();

                Console.WriteLine($"[API REST] Filtrados:  {centros.Count} centros");
                return centros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error API REST filtrados: {ex.Message}");
                return await GetCentrosFiltradosLocalAsync(filter);
            }
        }

        // Filtrado local con LINQ (desarrollo)
        private async Task<List<Centro>> GetCentrosFiltradosLocalAsync(CentroFilter filter)
        {
            var centros = await GetAllCentrosAsync();

            if (filter.IsEmpty())
                return centros.OrderBy(c => c.NOM).ToList();

            var query = centros.AsQueryable();

            if (!string.IsNullOrEmpty(filter.DTITUC))
                query = query.Where(c => c.DTITUC == filter.DTITUC);

            if (!string.IsNullOrEmpty(filter.DTERRE))
                query = query.Where(c => c.DTERRC == filter.DTERRE);

            if (!string.IsNullOrEmpty(filter.DMUNIC))
                query = query.Where(c => c.DMUNIC == filter.DMUNIC);

            return query.OrderBy(c => c.NOM).ToList();
        }

        public async Task<List<string>> GetTiposCentroAsync()
        {
            if (USE_API_REST)
            {
                try
                {
                    var response = await _httpClient.GetStringAsync($"{API_BASE_URL}/tipos");
                    return JsonSerializer.Deserialize<List<string>>(response) ?? new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error API REST tipos:  {ex.Message}");
                }
            }

            var centros = await GetAllCentrosAsync();
            return centros.Select(c => c.DTITUC).Distinct().OrderBy(t => t).ToList();
        }

        public async Task<List<string>> GetTerritoriosAsync(string dtituc = null)
        {
            if (USE_API_REST)
            {
                try
                {
                    var query = !string.IsNullOrEmpty(dtituc) ? $"?dtituc={Uri.EscapeDataString(dtituc)}" : "";
                    var response = await _httpClient.GetStringAsync($"{API_BASE_URL}/territorios{query}");
                    return JsonSerializer.Deserialize<List<string>>(response) ?? new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error API REST territorios: {ex.Message}");
                }
            }

            var centros = await GetAllCentrosAsync();
            var query = centros.AsQueryable();
            if (!string.IsNullOrEmpty(dtituc))
                query = query.Where(c => c.DTITUC == dtituc);
            return query.Select(c => c.DTERRC).Distinct().OrderBy(t => t).ToList();
        }

        public async Task<List<string>> GetMunicipiosAsync(string dterre = null)
        {
            if (USE_API_REST)
            {
                try
                {
                    var query = !string.IsNullOrEmpty(dterre) ? $"?dterre={Uri.EscapeDataString(dterre)}" : "";
                    var response = await _httpClient.GetStringAsync($"{API_BASE_URL}/municipios{query}");
                    return JsonSerializer.Deserialize<List<string>>(response) ?? new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error API REST municipios: {ex.Message}");
                }
            }

            var centros = await GetAllCentrosAsync();
            var query = centros.AsQueryable();
            if (!string.IsNullOrEmpty(dterre))
                query = query.Where(c => c.DTERRC == dterre);
            return query.Select(c => c.DMUNIC).Distinct().OrderBy(m => m).ToList();
        }

        public async Task<Centro> GetCentroByCodigoAsync(int ccen)
        {
            if (USE_API_REST)
            {
                try
                {
                    var response = await _httpClient.GetStringAsync($"{API_BASE_URL}/{ccen}");
                    return JsonSerializer.Deserialize<Centro>(response, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = false
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error API REST centro: {ex.Message}");
                }
            }

            var centros = await GetAllCentrosAsync();
            return centros.FirstOrDefault(c => c.CCEN == ccen);
        }

        // Métodos legacy
        public async Task<List<string>> GetMunicipiosAsync() => await GetMunicipiosAsync(null);
        public async Task<List<string>> GetProvinciasAsync() => await GetTerritoriosAsync(null);
    }
}