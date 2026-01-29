using Newtonsoft.Json;

namespace Reto2FINAL.Models
{
    public class CentrosResponse
    {
        [JsonProperty("CENTROS")]
        public List<Centro> Centros { get; set; } = new List<Centro>();
    }

    public class Centro
    {
        [JsonProperty("CCEN")]
        public int Codigo { get; set; }

        [JsonProperty("NOM")]
        public string NOMBRE { get; set; } = string.Empty;

        [JsonProperty("NOME")]
        public string NombreEuskera { get; set; } = string.Empty;

        [JsonProperty("DMUNIC")]
        public string DMUNIC { get; set; } = string.Empty;

        [JsonProperty("DMUNIE")]
        public string MunicipioEuskera { get; set; } = string.Empty;

        [JsonProperty("DTERRE")]
        public string DTERRE { get; set; } = string.Empty;

        [JsonProperty("DTERRC")]
        public string TerritorioEuskera { get; set; } = string.Empty;

        [JsonProperty("DTITUC")]
        public string DTITUC { get; set; } = string.Empty;

        [JsonProperty("DTITUE")]
        public string TipoEuskera { get; set; } = string.Empty;

        [JsonProperty("DOMI")]
        public string DIRECCION { get; set; } = string.Empty;

        [JsonProperty("TEL1")]
        public long TELEFONO { get; set; }

        [JsonProperty("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("PAGINA")]
        public string PaginaWeb { get; set; } = string.Empty;

        [JsonProperty("CPOS")]
        public int CodigoPostal { get; set; }

        [JsonProperty("LATITUD")]
        public double LATITUD { get; set; }

        [JsonProperty("LONGITUD")]
        public double LONGITUD { get; set; }

        // Para compatibilidad con el resto del código
        public string CODIGO => Codigo.ToString();
        public string LOCALIDAD => DMUNIC; 

        // Propiedades computadas para coordenadas 
        public double LatitudNumeric => LONGITUD;
        public double LongitudNumeric => LATITUD;

        // Normalizar territorio
        public string TerritorioNormalizado
        {
            get
            {
                if (string.IsNullOrEmpty(DTERRE)) return string.Empty;

                // ARABA/ÁLAVA → Araba
                // BIZKAIA → Bizkaia
                // GIPUZKOA → Gipuzkoa
                var territorio = DTERRE.Split('/')[0].Trim();

                return territorio switch
                {
                    "ARABA" => "Araba",
                    "ÁLAVA" => "Araba",
                    "BIZKAIA" => "Bizkaia",
                    "VIZCAYA" => "Bizkaia",
                    "GIPUZKOA" => "Gipuzkoa",
                    "GUIPÚZCOA" => "Gipuzkoa",
                    _ => territorio
                };
            }
        }
    }
}