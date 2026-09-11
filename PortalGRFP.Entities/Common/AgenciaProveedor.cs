using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class AgenciaProveedor
    {

        public int IdProveedor { get; set; }

        public int IdAgencia { get; set; }

        public bool OrdenCompraAutomatica { get; set; }

        public bool FacturaAutomatica { get; set; }

        public bool CatalogoAutomatico { get; set; }

        public bool PermiteRemisiones { get; set; }
        public bool RespuestaFaltante { get; set; }
    }


    public class AgenciaProveedorExt : AgenciaProveedor
    {
        public string Proveedor { get; set; }

        public string Agencia { get; set; }

    }

}
