using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ListadoSugerido
    {
        public string FechaCreacionSugerido { get; set; }
        public Int64 IdSugerido { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public string Proveedor { get; set; }
        public string Articulo_Id { get; set; }
        public string Producto { get; set; }
        public decimal CostoNeto { get; set; }
        public string TipoPedido { get; set; }
        public int ExistenciaPza { get; set; }
        public int TransitoPza { get; set; }
        public decimal SugeridoInicial { get; set; }
        public int NegadosPza { get; set; }
        public int CompraEspecialPza { get; set; }
        public int InvenadroPza { get; set; }
        public int Capacity { get; set; }
        public int PedEspFarmacia { get; set; }
        public int PolABCDias { get; set; }
        public string ABC { get; set; }
        public string TipoCalculo { get; set; }
        public int FactorEmpaque { get; set; }
        public decimal PedidoFinal { get; set; }
        public decimal PedidoFinalMod { get; set; }
        public decimal PVD { get; set; }

    }


    public class DetalladoSugerido
    {
        public string FechaSugerido { get; set; }
        public Int64 IdSugerido { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public string Proveedor { get; set; }
        public string Articulo_Id { get; set; }
        public string Producto { get; set; }
        public decimal CostoNeto { get; set; }
        public string TipoPedido { get; set; }
        public decimal Cantidad { get; set; }
    }

    public class DetalleSugeridoMontos
    {
        public decimal MontosPedido { get; set; }
        public decimal MontoPiezas { get; set; }
        public decimal MontoFueraLimite { get; set; }
    }

    public class ConsultaSugerido
    {
        public List<DetalleSugeridoMontos> Montos { get; set; }
        public List<ListadoSugerido> Listado { get; set; }

    }
}
