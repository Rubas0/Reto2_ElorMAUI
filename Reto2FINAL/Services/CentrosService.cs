using Reto2FINAL.Models;
using Newtonsoft.Json;

namespace Reto2FINAL.Services
{
    public class CentrosService
    {
        private readonly HttpClient _httpClient;
        private List<Centro>? _centrosCache;
        private const string JSON_URL = "http://10.5.104.110:8080/EuskadiLatLon.json";

        public CentrosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<List<Centro>> ObtenerTodosCentrosAsync()
        {
            if (_centrosCache != null)
                return _centrosCache;

            try
            {
                var response = await _httpClient.GetStringAsync(JSON_URL);
                var centrosResponse = JsonConvert.DeserializeObject<CentrosResponse>(response);

                if (centrosResponse?.Centros != null)
                {
                    _centrosCache = centrosResponse.Centros;
                }
                else
                {
                    _centrosCache = new List<Centro>();
                }

                return _centrosCache;
            }
            catch (HttpRequestException)
            {
                return new List<Centro>();
            }
            catch (TaskCanceledException)
            {
                return new List<Centro>();
            }
            catch (JsonException)
            {
                return new List<Centro>();
            }
            catch (Exception)
            {
                return new List<Centro>();
            }
        }

        // LINQ: Obtener tipos de centro distintos y ordenados (DTITUC)
        public async Task<List<string>> ObtenerTiposCentroAsync()
        {
            var centros = await ObtenerTodosCentrosAsync();
            return centros
                .Select(c => c.DTITUC)
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }

        // LINQ: Obtener territorios por tipo de centro (DTERRE normalizado)
        public async Task<List<string>> ObtenerTerritoriosPorTipoAsync(string? tipoCentro)
        {
            var centros = await ObtenerTodosCentrosAsync();

            var query = centros.AsQueryable();

            if (!string.IsNullOrEmpty(tipoCentro))
                query = query.Where(c => c.DTITUC == tipoCentro);

            return query
                .Select(c => c.TerritorioNormalizado)
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }

        // LINQ: Obtener municipios por territorio
        public async Task<List<string>> ObtenerMunicipiosPorTerritorioAsync(string? tipoCentro, string? territorio)
        {
            var centros = await ObtenerTodosCentrosAsync();

            var query = centros.AsQueryable();

            if (!string.IsNullOrEmpty(tipoCentro))
                query = query.Where(c => c.DTITUC == tipoCentro);

            if (!string.IsNullOrEmpty(territorio))
                query = query.Where(c => c.TerritorioNormalizado == territorio);

            return query
                .Select(c => c.DMUNIC)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m)
                .ToList();
        }

        // LINQ: Filtrar centros
        public async Task<List<Centro>> FiltrarCentrosAsync(string? tipoCentro, string? territorio, string? municipio)
        {
            var centros = await ObtenerTodosCentrosAsync();

            var query = centros.AsQueryable();

            if (!string.IsNullOrEmpty(tipoCentro))
                query = query.Where(c => c.DTITUC == tipoCentro);

            if (!string.IsNullOrEmpty(territorio))
                query = query.Where(c => c.TerritorioNormalizado == territorio);

            if (!string.IsNullOrEmpty(municipio))
                query = query.Where(c => c.DMUNIC == municipio);

            return query.ToList();
        }

        // Obtener centro por código
        public async Task<Centro?> ObtenerCentroPorCodigoAsync(string codigo)
        {
            var centros = await ObtenerTodosCentrosAsync();
            return centros.FirstOrDefault(c => c.CODIGO == codigo);
        }
    }
}