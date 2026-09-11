using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class LogExclusiones
    {
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public string Laboratorio { get; set; }
        public string Error { get; set; }

    }

    public class BitacoraExclusiones
    {
        public List<LogExclusiones> Exclusiones { get; set; }
        public List<LogExclusiones> LogExclusiones { get; set; }
    }


}
