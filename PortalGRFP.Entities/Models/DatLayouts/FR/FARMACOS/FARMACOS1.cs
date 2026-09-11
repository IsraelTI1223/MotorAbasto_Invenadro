using PortalGRFP.Entities.Attributes;
using PortalGRFP.Entities.Enums;

namespace PortalGRFP.Entities.Models.DatLayouts.FR.FARMACOS
{
    /// <summary>
    /// Layout de archivo .txt o .dat con cadena de 142 caracteres, 
    /// para FR -> FARMACOS | FANASA
    /// </summary>
    public class FARMACOS1 : LayoutBase
    {
        [DatLayout(0, 13)]
        public string CodigoEAN { get; set; }
        [DatLayout(13, 31)]
        public string Descripcion { get; set; }
        [DatLayout(44, 10)]
        public decimal PMP { get; set; }
        [DatLayout(54, 10)]
        public decimal PrecioFarmacia { get; set; }
        [DatLayout(64, 10)]
        public decimal Descuento { get; set; }
        [DatLayout(74, 10)]
        public int IVA { get; set; }
        [DatLayout(84, 31)]
        public string NombreLaboratorio { get; set; }
        [DatLayout(116, 7)]
        public long Existencia { get; set; }
        [DatLayout(122, 10)]
        public decimal IEPS { get; set; }
        [DatLayout(132, 10)]
        public decimal Oferta { get; set; }
        public FARMACOS1()
        {
            TipoLayout = LayoutTypes.FARMACOS142;
            TipoCargas = TipoCargas.PMP_FR;
        }
    }
}