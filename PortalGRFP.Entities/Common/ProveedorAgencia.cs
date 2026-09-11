using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ProveedorAgencia
    {
        public int IdProveedor { get; set; }

        public int IdCliente { get; set; }

        public int IdAgencia { get; set; }

        public string Agencia { get; set; }
    }
}
