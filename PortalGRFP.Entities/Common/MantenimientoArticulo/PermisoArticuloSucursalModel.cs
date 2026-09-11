using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.MantenimientoArticulo
{
    public class PermisoArticuloSucursalModel
    {
        public string Cadena { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public string SKU { get; set; }
        public string DESCRIPCION { get; set; }
        public string ActivoCompra { get; set; }
        public string ActivoVenta { get; set; }
    }
}
