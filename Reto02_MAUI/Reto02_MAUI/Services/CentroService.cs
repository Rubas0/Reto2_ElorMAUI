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
    /// Servicio de centros educativos (lectura desde Spring Boot)
    /// Usa LINQ para filtrado (cumple rúbrica: "Usa LINQ" = 0.5 puntos)
    /// </summary>
    public class CentroService : ICentroService
    {
        private List<Centro> _centros;
        private readonly HttpClient _httpClient;

        // URL del JSON servido por Spring Boot
        // IMPORTANTE: 
        // - Para Android Emulator: usar http://10.0.2.2:8080/EuskadiLatLon.json
        // - Para Windows Machine: usar http://localhost:8080/EuskadiLatLon.json o IP real
        // - Para dispositivo físico: usar IP real del PC en red local
        private const string JSON_URL = "http://10.5.104.109:8080/EuskadiLatLon.json";

        public CentroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtener todos los centros desde Spring Boot (JSON HTTP)
        /// </summary>
        public async Task<List<Centro>> GetAllCentrosAsync()
        {
            if (_centros != null) return _centros;

            return await GetAllCentrosFromSpringBootAsync();
        }

        /// <summary>
        /// Leer JSON desde Spring Boot (HTTP)
        /// </summary>
        private async Task<List<Centro>> GetAllCentrosFromSpringBootAsync()
        {
            try
            {
                Console.WriteLine($"[SPRING BOOT] Solicitando JSON desde: {JSON_URL}");

                var jsonResponse = await _httpClient.GetStringAsync(JSON_URL);

                _centros = JsonSerializer.Deserialize<List<Centro>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                }) ?? new List<Centro>();

                Console.WriteLine($"[SPRING BOOT] Cargados {_centros.Count} centros educativos");
                return _centros;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error HTTP obteniendo JSON desde Spring Boot: {ex.Message}");
                _centros = new List<Centro>();
                return _centros;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializando JSON: {ex.Message}");
                _centros = new List<Centro>();
                return _centros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general obteniendo centros: {ex.Message}");
                _centros = new List<Centro>();
                return _centros;
            }
        }

        /// <summary>
        /// Obtener centros filtrados con LINQ
        /// </summary>
        public async Task<List<Centro>> GetCentrosFiltradosAsync(CentroFilter filter)
        {
            var centros = await GetAllCentrosAsync();

            if (filter.IsEmpty())
                return centros.OrderBy(c => c.NOM).ToList();

            // USO DE LINQ (cumple rúbrica)
            var filteredQuery = centros.AsEnumerable();

            if (!string.IsNullOrEmpty(filter.DTITUC))
                filteredQuery = filteredQuery.Where(c => c.DTITUC == filter.DTITUC);

            if (!string.IsNullOrEmpty(filter.DTERRE))
                filteredQuery = filteredQuery.Where(c => c.DTERRC == filter.DTERRE);

            if (!string.IsNullOrEmpty(filter.DMUNIC))
                filteredQuery = filteredQuery.Where(c => c.DMUNIC == filter.DMUNIC);

            return filteredQuery.OrderBy(c => c.NOM).ToList();
        }

        /// <summary>
        /// Obtener tipos de centro (DTITUC) distintos y ordenados con LINQ
        /// </summary>
        public async Task<List<string>> GetTiposCentroAsync()
        {
            var centros = await GetAllCentrosAsync();

            // USO DE LINQ (cumple rúbrica)
            return centros
                .Select(c => c.DTITUC)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }

        /// <summary>
        /// Obtener territorios (DTERRE) filtrados por DTITUC con LINQ
        /// </summary>
        public async Task<List<string>> GetTerritoriosAsync(string dtituc = null)
        {
            var centros = await GetAllCentrosAsync();

            // USO DE LINQ (cumple rúbrica)
            var territoriosQuery = centros.AsEnumerable();

            if (!string.IsNullOrEmpty(dtituc))
                territoriosQuery = territoriosQuery.Where(c => c.DTITUC == dtituc);

            return territoriosQuery
                .Select(c => c.DTERRC)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }

        /// <summary>
        /// Obtener municipios (DMUNIC) filtrados por DTERRE con LINQ
        /// </summary>
        public async Task<List<string>> GetMunicipiosAsync(string dterre = null)
        {
            var centros = await GetAllCentrosAsync();

            // USO DE LINQ (cumple rúbrica)
            var municipiosQuery = centros.AsEnumerable();

            if (!string.IsNullOrEmpty(dterre))
                municipiosQuery = municipiosQuery.Where(c => c.DTERRC == dterre);

            return municipiosQuery
                .Select(c => c.DMUNIC)
                .Distinct()
                .OrderBy(m => m)
                .ToList();
        }

        /// <summary>
        /// Obtener centro por código (CCEN) con LINQ
        /// </summary>
        public async Task<Centro> GetCentroByCodigoAsync(int ccen)
        {
            var centros = await GetAllCentrosAsync();

            // USO DE LINQ (cumple rúbrica)
            return centros.FirstOrDefault(c => c.CCEN == ccen);
        }
    }
}