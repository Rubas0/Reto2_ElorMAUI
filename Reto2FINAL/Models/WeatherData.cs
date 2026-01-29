using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Reto2FINAL.Models
{
    public class WeatherApiResponse
    {
        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        [JsonPropertyName("current")]
        public CurrentWeatherApi? Current { get; set; }

        [JsonPropertyName("forecast")]
        public Forecast? Forecast { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("lat")]
        public double? Lat { get; set; }

        [JsonPropertyName("lon")]
        public double? Lon { get; set; }

        [JsonPropertyName("tz_id")]
        public string Timezone { get; set; } = string.Empty;

        [JsonPropertyName("localtime")]
        public string Localtime { get; set; } = string.Empty;
    }

    public class CurrentWeatherApi
    {
        [JsonPropertyName("last_updated")]
        public string? LastUpdated { get; set; }

        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }

        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }

        [JsonPropertyName("condition")]
        public WeatherCondition? Condition { get; set; }

        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }

        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }

        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDir { get; set; } = string.Empty;

        [JsonPropertyName("pressure_mb")]
        public double PressureMb { get; set; }

        [JsonPropertyName("precip_mm")]
        public double PrecipMm { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("cloud")]
        public int Cloud { get; set; }

        [JsonPropertyName("feelslike_c")]
        public double FeelsLikeC { get; set; }

        [JsonPropertyName("vis_km")]
        public double VisKm { get; set; }

        [JsonPropertyName("gust_kph")]
        public double GustKph { get; set; }

        [JsonPropertyName("uv")]
        public double Uv { get; set; }
    }

    public class WeatherCondition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class Forecast
    {
        [JsonPropertyName("forecastday")]
        public List<ForecastDay>? ForecastDay { get; set; }
    }

    public class ForecastDay
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("date_epoch")]
        public long DateEpoch { get; set; }

        [JsonPropertyName("day")]
        public DayForecast? Day { get; set; }

        // opcional: horas si quieres detalle por hora
        [JsonPropertyName("hour")]
        public List<HourForecast>? Hour { get; set; }
    }

    public class DayForecast
    {
        [JsonPropertyName("maxtemp_c")]
        public double MaxTempC { get; set; }

        [JsonPropertyName("mintemp_c")]
        public double MinTempC { get; set; }

        [JsonPropertyName("avgtemp_c")]
        public double AvgTempC { get; set; }

        [JsonPropertyName("maxwind_kph")]
        public double MaxWindKph { get; set; }

        [JsonPropertyName("totalprecip_mm")]
        public double TotalPrecipMm { get; set; }

        [JsonPropertyName("avgvis_km")]
        public double AvgVisKm { get; set; }

        [JsonPropertyName("avghumidity")]
        public double AvgHumidity { get; set; }

        [JsonPropertyName("condition")]
        public WeatherCondition? Condition { get; set; }

        [JsonPropertyName("uv")]
        public double Uv { get; set; }
    }

    public class HourForecast
    {
        [JsonPropertyName("time")]
        public string Time { get; set; } = string.Empty;

        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }

        [JsonPropertyName("condition")]
        public WeatherCondition? Condition { get; set; }

        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    // Muestra un día simplificado
    public class WeatherDay
    {
        public string Date { get; set; } = string.Empty;
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string WeatherDescription { get; set; } = string.Empty;
    }
}