using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class DetalleInputVenta
    {
        public int IDTienda { get; set; }
        public string Tienda { get; set; }
        public string Cadena { get; set; }
        public string Status_Venta { get; set; }
        public decimal Venta_Piezas { get; set; }
        public decimal Venta_Historica { get; set; }
        public string Diferencia { get; set; }
        public string Fecha_Venta { get; set; }
        public string Status_Existencia { get; set; }
        public decimal Existencia_Piezas { get; set; }
        public string Fecha_Existencias { get; set; }
    }
}
