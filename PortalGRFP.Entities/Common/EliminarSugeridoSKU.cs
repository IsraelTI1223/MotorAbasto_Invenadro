using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class EliminarSugeridoSKU
    {
        public long IdSugerido { get; set; }
        public int Farmacia { get; set; }
        public string Proveedor { get; set; }
        public string SKU { get; set; }
    }
}
