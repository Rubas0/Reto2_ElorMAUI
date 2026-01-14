using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reto02_MAUI.Models;

namespace Reto02_MAUI.Services
{
    public interface ICentroService
    {
        Task<List<Centro>> GetAllCentrosAsync();
        Task<List<Centro>> GetCentrosFiltradosAsync(CentroFilter filter);
        Task<List<string>> GetTiposCentroAsync();

        Task<List<string>> GetTerritoriosAsync(string dtituc = null);
        Task<List<string>> GetMunicipiosAsync(string dterre = null);

        Task<Centro> GetCentroByCodigoAsync(int ccen);

        Task<List<string>> GetMunicipiosAsync();
        Task<List<string>> GetProvinciasAsync();
    }
}