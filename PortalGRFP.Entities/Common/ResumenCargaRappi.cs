using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PortalGRFP.Entities.Common
{
    public class ResumenCargaRappi
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Lineas { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaCarga { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
