using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ReporteArticulosProveedorModel
    {
        public int IdProveedor { get; set; }
        public int IdAgencia { get; set; }
        public string Sku { get; set; }
        public string DescripcionCorta { get; set; }
        public int Existencia { get; set; }
        public decimal Costo { get; set; }
        public string Controlado { get; set; }
        public string Refrigerado { get; set; }
        public decimal PMP { get; set; }
        public int Empaque { get; set; }

    }
}
