using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Sugerido
{
    public class OrdenCompraModel
    {
        //						

        public long IdSugerido { get; set; }
        public int Orden_Compra { get; set; }
        public int Id_Sucursal { get; set; }
        public int IdProveedor { get; set; }
        public string Nombre_Sucursal { get; set; }
        public string Proveedor { get; set; }
        public decimal Importe { get; set; }
        public string Estatus { get; set; }

    }
}
