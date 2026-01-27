using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto2FINAL.Models
{
    /// <summary>
    /// Representa un centro educativo de Euskadi desde EuskadiLatLon.json
    /// </summary>
    public class Centro
    {
        public string Codigo { get; set; } = string.Empty;
        public string DTITUC { get; set; } = string.Empty;
        public string DTERRE { get; set; } = string.Empty;
        public string DMUNIC { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
