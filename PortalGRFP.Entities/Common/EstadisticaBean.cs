using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class EstadisticaBean
    {
        public List<EstadisticaCompras> Compras { get; set; }
        public List<EstadisticaProveedor> Proveedor { get; set; }
        public List<EstadisticaVenta28dias> Ventas28 { get; set; }
    }
}
