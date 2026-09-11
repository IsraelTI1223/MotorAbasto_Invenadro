using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class SucursalProveedor
    {
        public int IdProveedor { get; set; }

        public int IdSucursal { get; set; }

        public int LeadTime { get; set; }

        public int DiasCobertura { get; set; }

        public int IdAgencia { get; set; }

        public int ClienteProveedor { get; set; }

        public int Lunes { get; set; }

        public int Martes { get; set; }

        public int Miercoles { get; set; }
        public int Jueves { get; set; }
        public int Viernes { get; set; }
        public int Sabado { get; set; }
        public int Domingo { get; set; }
    }

    public class SucursalProveedorExt : SucursalProveedor
    {
        public string Calendario { get; set; }

        public string NomAgencia { get; set; }

        public string NomProveedor { get; set; }
    }
}
