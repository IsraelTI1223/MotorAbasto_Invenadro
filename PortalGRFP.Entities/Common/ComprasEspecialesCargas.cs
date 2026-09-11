using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ComprasEspecialesCargas
    {
        public List<PedidosCargados> PedidosCargardosB { get; set; }

        public List<Rechazados> RechazadosB { get; set; }
    }

    public class PedidosCargados
    {
        public Int64 Folio { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoPedido { get; set; }

        public int LineasPedido { get; set; }
        public int LineasCargadas { get; set; }
        public string Estatus { get; set; }
    }

    public class Rechazados
    {
        public Int64 Folio { get; set; }
        public string Proveedor { get; set; }

        public string FechaCarga { get; set; }

        public string FechaAplicacion { get; set; }
        public string Tipo { get; set; }

        public string Farmacia { get; set; }

        public string SKU { get; set; }

        public string Descripcion { get; set; }

        public String TipoIncidencia { get; set; }
    }
}
