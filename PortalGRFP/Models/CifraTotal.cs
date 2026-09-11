using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalGRFP
{
    public class CifraTotal
    {
        public string fecha_ped { get; set; }
        public decimal pedido { get; set; }
        public decimal cantidadpedida { get; set; }
        public decimal pedido_conf { get; set; }
        public decimal cantidadconf { get; set; } 
        public decimal pedido_fact { get; set; }
        public decimal cantidadfact { get; set; }

        public decimal pedido_conf_suc { get; set; }

        public decimal cantidadRecSuc { get; set; }


    }

}