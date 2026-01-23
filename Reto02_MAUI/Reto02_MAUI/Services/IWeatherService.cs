using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto02_MAUI.Models;

namespace Reto02_MAUI.Services
{
    /// <summary>
    /// Interfaz del servicio meteorológico (Geoapify Weather API)
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Obtener información meteorológica por coordenadas usando Geoapify
        /// </summary>
        Task<WeatherInfo> GetWeatherByCoordinatesAsync(string latitud, string longitud);
    }
}