using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto2FINAL.Models;

namespace Reto2FINAL.Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Obtiene datos de clima actual y previsión desde Open-Meteo API
        /// </summary>
        Task<WeatherResponse?> ObtenerClimaPorCoordendasAsync(float latitud, float longitud);
    }
}
