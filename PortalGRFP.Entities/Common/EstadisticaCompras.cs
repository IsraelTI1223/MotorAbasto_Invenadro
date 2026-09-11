using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class EstadisticaCompras
    {
        public int SucId { get; set; }
        public string Sucursal { get; set; }
        public int Existencia { get; set; }
        public int Transito { get; set; }
        public decimal PVD { get; set; }
        public string ABC { get; set; }
        public int PoliticaDias { get; set; }
        public int Invenadro { get; set; }
        public int StockMin { get; set; }
        public string Activo { get; set; }
        public string FechaUltimaVenta { get; set; }

    }

    public class EstadisticaProveedor
    {
        public string Agencia { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public int Empaque { get; set; }
        public decimal Costo { get; set; }
    }

    public class EstadisticaVenta28dias
    {
        public int Dia { get; set; }
        public int Contado { get; set; }
        public int Credito { get; set; }
        public int Sad { get; set; }
        public int Total { get; set; }
    }

    
}
