using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Reto02_MAUI.Models
{
    /// <summary>
    /// Modelo de Centro Educativo de Euskadi
    /// Corresponde a la estructura del JSON servido por Spring Boot
    /// http://10.5.104.109:8080/EuskadiLatLon.json
    /// </summary>
    public class Centro
    {
        [JsonPropertyName("CCEN")]
        public int CCEN { get; set; }

        [JsonPropertyName("CLOCALI")]
        public int CLOCALI { get; set; }

        [JsonPropertyName("CMUNIC")]
        public int CMUNIC { get; set; }

        [JsonPropertyName("CPOS")]
        public string CPOS { get; set; }

        [JsonPropertyName("CTERRC")]
        public int CTERRC { get; set; }

        [JsonPropertyName("CVIA")]
        public int CVIA { get; set; }

        [JsonPropertyName("DGENRC")]
        public string DGENRC { get; set; }

        [JsonPropertyName("DLOCALI")]
        public string DLOCALI { get; set; }

        [JsonPropertyName("DMUNIC")]
        public string DMUNIC { get; set; }

        [JsonPropertyName("DOMI")]
        public string DOMI { get; set; }

        [JsonPropertyName("DTERRC")]
        public string DTERRC { get; set; }

        [JsonPropertyName("DTIPVIA")]
        public string DTIPVIA { get; set; }

        [JsonPropertyName("DTITUC")]
        public string DTITUC { get; set; }

        [JsonPropertyName("EMAIL")]
        public string EMAIL { get; set; }

        [JsonPropertyName("FAX")]
        public string FAX { get; set; }

        [JsonPropertyName("LATITUD")]
        public string LATITUD { get; set; }

        [JsonPropertyName("LONGITUD")]
        public string LONGITUD { get; set; }

        [JsonPropertyName("NOM")]
        public string NOM { get; set; }

        [JsonPropertyName("NVIA")]
        public string NVIA { get; set; }

        [JsonPropertyName("PAGINA")]
        public string PAGINA { get; set; }

        [JsonPropertyName("PVIA")]
        public string PVIA { get; set; }

        [JsonPropertyName("TEL")]
        public string TEL { get; set; }

        [JsonPropertyName("WEB")]
        public string WEB { get; set; }

        /// <summary>
        /// Dirección completa formateada
        /// </summary>
        public string DireccionCompleta =>
            $"{DTIPVIA} {NVIA} {PVIA}, {CPOS} {DMUNIC} ({DTERRC})";
    }
}