using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto02_MAUI.Models;

namespace Reto02_MAUI.Services
{
    /// <summary>
    /// Interfaz del servicio de centros educativos
    /// </summary>
    public interface ICentroService
    {
        /// <summary>
        /// Obtener todos los centros desde Spring Boot (JSON HTTP)
        /// </summary>
        Task<List<Centro>> GetAllCentrosAsync();

        /// <summary>
        /// Obtener centros filtrados según criterios LINQ
        /// </summary>
        Task<List<Centro>> GetCentrosFiltradosAsync(CentroFilter filter);

        /// <summary>
        /// Obtener todos los tipos de centro (DTITUC) distintos y ordenados
        /// </summary>
        Task<List<string>> GetTiposCentroAsync();

        /// <summary>
        /// Obtener todos los territorios (DTERRE) filtrados por DTITUC
        /// </summary>
        Task<List<string>> GetTerritoriosAsync(string dtituc = null);

        /// <summary>
        /// Obtener todos los municipios (DMUNIC) filtrados por DTERRE
        /// </summary>
        Task<List<string>> GetMunicipiosAsync(string dterre = null);

        /// <summary>
        /// Obtener centro por código (CCEN)
        /// </summary>
        Task<Centro> GetCentroByCodigoAsync(int ccen);
    }
}