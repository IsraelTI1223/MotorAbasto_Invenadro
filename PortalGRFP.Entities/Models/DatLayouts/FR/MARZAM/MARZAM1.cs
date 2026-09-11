using PortalGRFP.Entities.Attributes;
using PortalGRFP.Entities.Enums;
using System;

namespace PortalGRFP.Entities.Models.DatLayouts.FR.MARZAM
{
    public class MARZAM1 : LayoutBase
    {        
        [DatLayout(0, 8)]
        public string Fecha { get; set; }
        [DatLayout(8, 9)]
        public string CodigoProducto { get; set; }
        [DatLayout(17, 40)]
        public string NombreProducto { get; set; }
        [DatLayout(57, 10)]
        public decimal PrecioFarmacia { get; set; }
        [DatLayout(67, 10)]
        public decimal PrecioPublico { get; set; }
        [DatLayout(77, 6)]
        public decimal IVA { get; set; }
        [DatLayout(83, 6)]
        public decimal IEPS { get; set; }
        [DatLayout(89, 6)]
        public decimal Impuesto3 { get; set; }
        [DatLayout(95, 2)]
        public string TipoProducto { get; set; }
        [DatLayout(97, 40)]
        public string LaboratorioFabricante { get; set; }
        [DatLayout(137, 2)]
        public string ClasificacionFiscal { get; set; }
        [DatLayout(139, 40)]
        public string DescripcionTerapeutica { get; set; }
        [DatLayout(179, 40)]
        public string SustanciaActiva { get; set; }
        [DatLayout(219, 1)]
        public string IdRefrigerado { get; set; }
        [DatLayout(220, 1)]
        public string IdControlado { get; set; }
        [DatLayout(221, 13)]
        public string CodigoEAN { get; set; }
        [DatLayout(234, 3)]
        public string UnidadVenta { get; set; }
        [DatLayout(237, 8)]
        public string FechaCaducidad { get; set; }
        [DatLayout(245, 2)]
        public string GrupoSSA { get; set; }
        [DatLayout(247, 1)]
        public string AccionArticulo { get; set; }
        [DatLayout(248, 4)]
        public long PzasEmpaqueOriginal { get; set; }
        [DatLayout(252, 6)]
        public decimal DesctoComercialProducto { get; set; }
        [DatLayout(258, 14)]
        public string Filler { get; set; }
        [DatLayout(272, 5)]
        public string NoRegistro { get; set; }
        public MARZAM1()
        {
            TipoLayout = LayoutTypes.MARZAM277;
            TipoCargas = TipoCargas.PMP_FR;
        }
    }
}