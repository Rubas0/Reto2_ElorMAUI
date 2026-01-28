using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto2FINAL.Models
{
    public class WeatherData
    {
        public CurrentWeather? Current { get; set; }
        public DailyForecast? Daily { get; set; }
    }

    public class CurrentWeather
    {
        public double Temperature_2m { get; set; }
        public int Weather_code { get; set; }
        public double Wind_speed_10m { get; set; }
    }

    public class DailyForecast
    {
        public List<string>? Time { get; set; }
        public List<double>? Temperature_2m_max { get; set; }
        public List<double>? Temperature_2m_min { get; set; }
        public List<int>? Weather_code { get; set; }
    }

    public class WeatherDay
    {
        public string Date { get; set; } = string.Empty;
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public int WeatherCode { get; set; }
        public string WeatherDescription => GetWeatherDescription(WeatherCode);

        private string GetWeatherDescription(int code)
        {
            return code switch
            {
                0 => "Despejado",
                1 or 2 or 3 => "Parcialmente nublado",
                45 or 48 => "Niebla",
                51 or 53 or 55 => "Llovizna",
                61 or 63 or 65 => "Lluvia",
                71 or 73 or 75 => "Nieve",
                95 => "Tormenta",
                _ => "Variado"
            };
        }
    }
}
