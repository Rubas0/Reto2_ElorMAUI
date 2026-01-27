using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto2FINAL.Helpers;
using Reto2FINAL.Models;
using Reto2FINAL.Services;

namespace Reto2FINAL.ViewModels
{
    [QueryProperty(nameof(CentroId), "id")]
    public partial class CentroDetailViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;
        private readonly IGeocodingService _geocodingService;
        private readonly ICentroService _centroService;

        [ObservableProperty]
        public Centro? centro;

        [ObservableProperty]
        public WeatherResponse? climaActual;

        [ObservableProperty]
        public List<DiaPrevisional> prevision = new();

        [ObservableProperty]
        public bool isLoading = false;

        [ObservableProperty]
        public string centroId = string.Empty;

        [ObservableProperty]
        public string mensajeError = string.Empty;

        public CentroDetailViewModel(
            IWeatherService weatherService,
            IGeocodingService geocodingService,
            ICentroService centroService)
        {
            _weatherService = weatherService;
            _geocodingService = geocodingService;
            _centroService = centroService;
        }

        [RelayCommand]
        public async Task CargarDetallesAsync()
        {
            IsLoading = true;
            MensajeError = string.Empty;

            try
            {
                if (Centro == null)
                    return;

                // Obtener datos de clima
                ClimaActual = await _weatherService.ObtenerClimaPorCoordendasAsync(
                    Centro.Latitud,
                    Centro.Longitud);

                if (ClimaActual != null)
                {
                    // Generar lista de previsión
                    GenerarPrevision();
                }
                else
                {
                    MensajeError = "No se pudieron obtener los datos del clima";
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Error cargando detalles: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void GenerarPrevision()
        {
            Prevision.Clear();

            if (ClimaActual?.daily == null || ClimaActual.daily.time.Length == 0)
                return;

            for (int i = 0; i < ClimaActual.daily.time.Length; i++)
            {
                Prevision.Add(new DiaPrevisional
                {
                    Fecha = ClimaActual.daily.time[i],
                    TempMin = ClimaActual.daily.temperature_2m_min[i],
                    TempMax = ClimaActual.daily.temperature_2m_max[i],
                    CodigoClima = ClimaActual.daily.weather_code[i],
                    DescripcionClima = WeatherCodeHelper.ConvertCode(ClimaActual.daily.weather_code[i]),
                    IconoClima = WeatherCodeHelper.GetWeatherIcon(ClimaActual.daily.weather_code[i]),
                    UVIndex = ClimaActual.daily.uv_index_max[i]
                });
            }
        }
    }

    /// <summary>
    /// Modelo auxiliar para mostrar cada día de la previsión
    /// </summary>
    public class DiaPrevisional
    {
        public string Fecha { get; set; } = string.Empty;
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public int CodigoClima { get; set; }
        public string DescripcionClima { get; set; } = string.Empty;
        public string IconoClima { get; set; } = string.Empty;
        public float UVIndex { get; set; }
    }
}
