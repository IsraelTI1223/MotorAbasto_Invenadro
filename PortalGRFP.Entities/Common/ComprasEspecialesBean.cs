using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ComprasEspecialesBean
    {
        public int TipoPedido { get; set; }
        public int TipoProveedor { get; set; }
        public DateTime FechaGenOC { get; set; }
        public Int64 Suc_Id { get; set; }

        public string Sku { get; set; }

        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

    }
}
