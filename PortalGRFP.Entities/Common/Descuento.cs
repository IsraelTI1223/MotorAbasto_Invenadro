using PortalGRFP.Entities.Enums;

namespace PortalGRFP.Entities.Common
{
    public class Descuento
    {
        public TipoCarga IdTipoDescuento { get; set; }
        public string CodigoEAN { get; set; }
        public decimal PMP { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoDescuento { get; set; }
        public bool Autorizacion { get; set; }
        public int Estatus { get; set; }
        public string TipoDescuento { get; set; }
        public string MsgCondiciones { get; set; }
        public string BasePrecio { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal PrecioFijo { get; set; }
        public decimal Margen { get; set; }
        public bool SinUtilidad { get; set; }
        public bool Excluir { get; set; }
    }
}
