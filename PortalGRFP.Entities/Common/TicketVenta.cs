using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class TicketVenta
    {
        public DateTime DiaOperacion { get; set; }
        public int NoSucursal { get; set; }
        public string Sucursal { get; set; }
        public float Subtotal { get; set; }
        public float Iva { get; set; }
        public float Total { get; set; }
        public string Ticket { get; set; }
    }
}
