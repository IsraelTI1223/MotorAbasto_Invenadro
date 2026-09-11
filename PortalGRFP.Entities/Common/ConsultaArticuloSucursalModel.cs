using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ConsultaArticuloSucursalModel
    {
        public int ID_Sucursal { get; set; }
        public string SKU { get; set; }
        public string DESCRIPCION_CORTA { get; set; }
        public string GRUPO { get; set; }
        public string FAMILIA { get; set; }
        public string Existencia { get; set; }
        public string PVD { get; set; }
        public string Fecha_Venta { get; set; }
        public string piezas_total { get; set; }
        
    }
}
