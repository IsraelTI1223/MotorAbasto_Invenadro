using PortalGRFP.Entities.Attributes;
using PortalGRFP.Entities.Enums;

namespace PortalGRFP.Entities.Models.DatLayouts.FR.NANDRO
{
    /// <summary>
    /// Layout del catálogo Productos
    /// </summary>
    public class NANDRO1 : LayoutBase
    {
        [DatLayout(0, 1)]
        public string TipoMovimiento { get; set; }
        [DatLayout(1, 8)]
        public string CodigoNadro { get; set; }
        [DatLayout(9, 1)]
        public int Familia { get; set; }
        [DatLayout(10, 1)]
        public string Departamento { get; set; }
        [DatLayout(11, 1)]
        public string Categoria { get; set; }
        [DatLayout(12, 1)]
        public string IdProductoDescto { get; set; }
        [DatLayout(13, 1)]
        public int IdVencimiento { get; set; }
        [DatLayout(14, 1)]
        public int IdRefrigeracion { get; set; }
        [DatLayout(15, 1)]
        public int? IdControlSSA { get; set; }
        [DatLayout(16, 1)]
        public int ClasificacionFiscal { get; set; }
        [DatLayout(17, 35)]
        public string Descripcio { get; set; }
        [DatLayout(52, 10)]
        public string NombreProveedor { get; set; }
        [DatLayout(62, 9)]
        public decimal PMPSinIVA { get; set; }
        [DatLayout(71, 9)]
        public decimal PrecioNetoSinIVA { get; set; }
        [DatLayout(80, 1)]
        public string IdProductoAntibiotico { get; set; }
        [DatLayout(81, 6)]
        public string FechaMovimiento { get; set; }
        [DatLayout(87, 13)]
        public string CodigoEAN { get; set; }
        [DatLayout(100, 14)]
        public decimal PreciofarmaciaIVA { get; set; }
        public NANDRO1()
        {
            TipoLayout = LayoutTypes.NANDRO114;
            TipoCargas = TipoCargas.PMP_FR;
        }
    }
}