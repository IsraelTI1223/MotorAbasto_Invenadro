using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class SugeridoCuenta
    {
        public Int64 idSugerido { get; set; }
        public int idSucursal { get; set; }
        public int id_proveedor { get; set; }
        public string CADENA { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string Desc_tienda { get; set; }
        public string Desc_proveedor { get; set; }

    }

}
