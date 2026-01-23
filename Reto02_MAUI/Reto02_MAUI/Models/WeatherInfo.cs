using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Reto02_MAUI.Models
{
    /// <summary>
    /// Información meteorológica de Geoapify Weather API
    /// https://www.geoapify.com/weather-api
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// Temperatura actual (°C)
        /// </summary>
        public double Temperatura { get; set; }

        /// <summary>
        /// Descripción del clima (ej: "cielo despejado", "lluvia ligera")
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Icono del clima (código o URL de Geoapify)
        /// </summary>
        public string Icono { get; set; }

        /// <summary>
        /// Humedad (%)
        /// </summary>
        public int Humedad { get; set; }

        /// <summary>
        /// Velocidad del viento (m/s)
        /// </summary>
        public double VelocidadViento { get; set; }

        /// <summary>
        /// Previsión para los próximos 5 días
        /// </summary>
        public List<PrevisionDia> Prevision { get; set; }

        /// <summary>
        /// URL del icono del clima (Geoapify proporciona URL completa)
        /// </summary>
        public string IconoUrl => Icono;

        public WeatherInfo()
        {
            Prevision = new List<PrevisionDia>();
        }
    }

    /// <summary>
    /// Previsión meteorológica para un día específico
    /// </summary>
    public class PrevisionDia
    {
        /// <summary>
        /// Fecha de la previsión
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Temperatura máxima (°C)
        /// </summary>
        public double TempMax { get; set; }

        /// <summary>
        /// Temperatura mínima (°C)
        /// </summary>
        public double TempMin { get; set; }

        /// <summary>
        /// Descripción del clima
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Icono del clima (código o URL)
        /// </summary>
        public string Icono { get; set; }

        /// <summary>
        /// URL del icono del clima
        /// </summary>
        public string IconoUrl => Icono;

        /// <summary>
        /// Día de la semana formateado (ej: "Lunes", "Martes")
        /// </summary>
        public string DiaSemana => Fecha.ToString("dddd");
    }
}