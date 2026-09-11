using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Sugerido
{
    public class ValidacionMontoSugeridoModel
    {
        public int Id { get; set; }
        public long IdSugerido { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public decimal MontoPromedio { get; set; }
        public decimal MontoPedido { get; set; }
        public decimal MontoFueraLimite { get; set; }
        public decimal PorcentajeVariacion { get; set; }
        public bool Autorizado { get; set; }
        public int PiezasXFarmacia { get; set; }

    }
}
