using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.ComprasEspeciales
{
    public class PedidosEspecialesDeleteModel
    {
        public long Folio { get; set; }
        public int Id_tipo { get; set; }
        public int Id_proveedor { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursales { get; set; }


    }
}
