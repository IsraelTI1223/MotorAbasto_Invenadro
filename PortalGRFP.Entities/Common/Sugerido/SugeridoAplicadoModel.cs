using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Sugerido
{
    public class SugeridoAplicadoModel
    {      
        public long IdSugerido { get; set; }
        //public int IdSucursal { get; set; }
        //public string Nombre { get; set; }
        public int IdProveedor { get; set; }
        public string Grupo { get; set; }
        public string Proveedor { get; set; }
        public int IdGrupo { get; set; }
        //public decimal pcio_Costo { get; set; }
        //public decimal CostoCalculado { get; set; }

    }
}
