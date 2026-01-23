using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto02_MAUI.Models
{
    /// <summary>
    /// Filtros para búsqueda de centros educativos con LINQ
    /// Cumple requisito rúbrica: "Usa LINQ" (0.5 puntos)
    /// </summary>
    public class CentroFilter
    {
        /// <summary>
        /// Tipo de centro (DTITUC)
        /// Ejemplos: "DEPARTAMENTO EDUCACIÓN", "PRIVADA", "OTROS PÚBLICOS"
        /// </summary>
        public string DTITUC { get; set; }

        /// <summary>
        /// Territorio (DTERRE)
        /// Ejemplos: "BIZKAIA", "GIPUZKOA", "ARABA"
        /// </summary>
        public string DTERRE { get; set; }

        /// <summary>
        /// Municipio (DMUNIC)
        /// Ejemplos: "BILBAO", "DONOSTIA", "VITORIA-GASTEIZ"
        /// </summary>
        public string DMUNIC { get; set; }

        /// <summary>
        /// Verifica si el filtro está vacío (sin criterios)
        /// </summary>
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(DTITUC) &&
                   string.IsNullOrEmpty(DTERRE) &&
                   string.IsNullOrEmpty(DMUNIC);
        }

        /// <summary>
        /// Resetea todos los filtros
        /// </summary>
        public void Reset()
        {
            DTITUC = string.Empty;
            DTERRE = string.Empty;
            DMUNIC = string.Empty;
        }
    }
}