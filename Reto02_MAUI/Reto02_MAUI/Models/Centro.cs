using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto02_MAUI.Models
{
    public class Centro
    {
        public int CCEN { get; set; }           // Código del centro
        public string NOM { get; set; }         // Nombre en castellano
        public string NOME { get; set; }        // Nombre en euskera
        public string DGENRC { get; set; }      // Género castellano
        public string DGENRE { get; set; }      // Género euskera
        public string GENR { get; set; }        // Código género
        public int MUNI { get; set; }           // Código municipio
        public string DMUNIC { get; set; }      // Municipio castellano
        public string DMUNIE { get; set; }      // Municipio euskera
        public string DTERRC { get; set; }      // Territorio castellano
        public string DTERRE { get; set; }      // Territorio euskera
        public int DEPE { get; set; }           // Código dependencia
        public string DTITUC { get; set; }      // Tipo castellano
        public string DTITUE { get; set; }      // Tipo euskera
        public string DOMI { get; set; }        // Domicilio
        public int CPOS { get; set; }           // Código postal
        public long TEL1 { get; set; }          // Teléfono
        public long TFAX { get; set; }          // Fax
        public string EMAIL { get; set; }       // Email
        public string PAGINA { get; set; }      // Página web
        public string COOR_X { get; set; }      // Coordenada X (no usaremos)
        public string COOR_Y { get; set; }      // Coordenada Y (no usaremos)
        public double LATITUD { get; set; }     //  En JSON es LONGITUD
        public double LONGITUD { get; set; }    //  En JSON es LATITUD

        //El JSON tiene INVERTIDAS latitud/longitud
        public double LatitudReal => LONGITUD;  
        public double LongitudReal => LATITUD; 

        // UI
        public string DisplayText => $"{NOM} ({DMUNIC})";
        public string UbicacionCompleta => $"{DMUNIC}, {DTERRC}";
        public string TelefonoFormateado => TEL1 > 0 ? TEL1.ToString() : "N/A";
        public string CoordenadasTexto => $"Lat: {LatitudReal:F4}, Lon: {LongitudReal:F4}";
    }
}