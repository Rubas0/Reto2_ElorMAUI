using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto02_MAUI.Helpers
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public string PageInfo => $"Página {PageIndex} de {TotalPages}";
        public string ResultsInfo => $"Mostrando {Items.Count} de {TotalCount} centros";

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>
            {
                Items = items,
                PageIndex = pageIndex,
                TotalPages = count == 0 ? 1 : (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
                TotalCount = count
            };
        }
    }
}