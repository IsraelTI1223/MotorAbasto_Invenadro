using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class PorcentajeProv
    {
        public int Id { get; set; }
        public string Proveedor { get; set; }
        public string Descr { get; set; }
        public int  Empates { get; set; }
        public int ProteccionCosto { get; set; }
        public int PlanCrecimiento { get; set; }
        public int NumProductos { get; set; }
    }
}
