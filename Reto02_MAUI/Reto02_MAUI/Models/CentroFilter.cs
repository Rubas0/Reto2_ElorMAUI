using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto02_MAUI.Models
{
    public class CentroFilter
    {
        public string DTITUC { get; set; }
        public string DTERRE { get; set; }
        public string DMUNIC { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(DTITUC) &&
                   string.IsNullOrEmpty(DTERRE) &&
                   string.IsNullOrEmpty(DMUNIC);
        }

        public void Clear()
        {
            DTITUC = null;
            DTERRE = null;
            DMUNIC = null;
        }
    }
}
