using Reto2FINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto2FINAL.Models;

namespace Reto2FINAL.Services
{
    public interface ICentroService
    {
        /// <summary>
        /// Carga todos los centros desde EuskadiLatLon.json
        /// </summary>
        Task<List<Centro>> CargarCentrosAsync();

        /// <summary>
        /// Obtiene valores distintos y ordenados de DTITUC
        /// </summary>
        List<string> ObtenerDTITUCDistintos(List<Centro> centros);

        /// <summary>
        /// Filtra centros por DTITUC y retorna DTERRE distintos ordenados
        /// </summary>
        List<string> ObtenerDTERREDistintos(List<Centro> centros, string dtituc);

        /// <summary>
        /// Filtra centros por DTITUC+DTERRE y retorna DMUNIC distintos ordenados
        /// </summary>
        List<string> ObtenerDMUNICDistintos(List<Centro> centros, string dtituc, string dterre);

        /// <summary>
        /// Filtra centros según criterios y retorna lista paginada
        /// </summary>
        (List<Centro> Centros, int Total) ObtenerCentrosFiltrados(
            List<Centro> centros,
            string dtituc,
            string dterre,
            string dmunic,
            int pageNumber = 1,
            int pageSize = 10);
    }
}
