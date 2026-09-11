using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ResumenRechazos
    {
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public int Configuracion { get; set; }
        public int Empaque { get; set; }
        public int Sku_Inactivo { get; set; }
        public int Descontinuado { get; set; }
        public int Licencia { get; set; }
        public int Regla_Negados { get; set; }
        public int Regla_Compra_Esp { get; set; }
        public int NoPublicado { get; set; }
        
    }
}
