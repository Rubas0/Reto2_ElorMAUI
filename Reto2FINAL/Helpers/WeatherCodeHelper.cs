using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto2FINAL.Helpers
{
    /// <summary>
    /// Conversión de WMO Weather codes a descripción legible
    /// Ref: https://artefacts.ceda.ac.uk/badc_datadocs/surface/code.html
    /// </summary>
    public static class WeatherCodeHelper
    {
        public static string ConvertCode(int code)
        {
            return code switch
            {
                0 => "Despejado",
                1 => "Mayormente despejado",
                2 => "Parcialmente nublado",
                3 => "Nublado",
                45 => "Niebla",
                48 => "Niebla con escarcha",
                51 => "Llovizna ligera",
                53 => "Llovizna moderada",
                55 => "Llovizna densa",
                61 => "Lluvia ligera",
                63 => "Lluvia moderada",
                65 => "Lluvia fuerte",
                71 => "Nieve ligera",
                73 => "Nieve moderada",
                75 => "Nieve fuerte",
                77 => "Granos de nieve",
                80 => "Chubascos ligeros",
                81 => "Chubascos moderados",
                82 => "Chubascos fuertes",
                85 => "Chubascos de nieve ligeros",
                86 => "Chubascos de nieve fuertes",
                95 => "Tormenta",
                96 => "Tormenta con granizo ligero",
                99 => "Tormenta con granizo fuerte",
                _ => "Desconocido"
            };
        }

        /// <summary>
        /// Le pone un icono muy chuli al lado
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>

        public static string GetWeatherIcon(int code)
        {
            return code switch
            {
                0 or 1 => "☀️",
                2 => "⛅",
                3 => "☁️",
                45 or 48 => "🌫️",
                51 or 53 or 55 => "🌧️",
                61 or 63 or 65 => "🌧️",
                71 or 73 or 75 or 77 => "❄️",
                80 or 81 or 82 => "🌧️",
                85 or 86 => "❄️",
                95 or 96 or 99 => "⛈️",
                _ => "🌡️"
            };
        }
    }
}
