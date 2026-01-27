using Reto2FINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto2FINAL.Services
{
    public interface IGeocodingService
    {
        /// <summary>
        /// Busca ciudades/lugares por nombre
        /// </summary>
        Task<GeocodingResponse?> BuscarCiudadAsync(string nombreCiudad);
    }
}
