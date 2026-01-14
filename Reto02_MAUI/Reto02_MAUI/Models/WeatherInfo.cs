using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto02_MAUI.Models
{
//Infor meteo actual y prev
    public class WeatherInfo
    {
        public string Ciudad { get; set; }
        public double TemperaturaActual { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public List<WeatherForecast> Prevision { get; set; } = new List<WeatherForecast>();

        // Propiedades calculadas para UI
        public string IconoUrl => !string.IsNullOrEmpty(Icono)
            ? $"https://openweathermap.org/img/wn/{Icono}@2x.png"
            : string.Empty;

        public string TemperaturaTexto => $"{TemperaturaActual:F1}°C";
    }

// Prev meteo 1 dia
    public class WeatherForecast
    {
        public DateTime Fecha { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }

        //Esto para meterlo en la UI
        public string FechaTexto => Fecha.ToString("ddd dd/MM");
        public string TempMaxTexto => $"{TempMax: F0}°";
        public string TempMinTexto => $"{TempMin: F0}°";
        public string IconoUrl => !string.IsNullOrEmpty(Icono)
            ? $"https://openweathermap.org/img/wn/{Icono}@2x.png"
            : string.Empty;
    }
}