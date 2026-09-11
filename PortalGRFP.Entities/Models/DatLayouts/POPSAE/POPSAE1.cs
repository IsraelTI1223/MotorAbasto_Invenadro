using PortalGRFP.Entities.Attributes;

namespace PortalGRFP.Entities.Models.DatLayouts.POPSAE
{
    public class POPSAE1 : LayoutBase
    {
        [DatLayout(0, 13)]
        public string SKU { get; set; }
        [DatLayout(13, 31)]
        public string Descripcion { get; set; }
        [DatLayout(44, 8)]
        public decimal PMP { get; set; }
        [DatLayout(52, 9)]
        public decimal IVA { get; set; }
        public POPSAE1()
        {
            TipoCargas = Enums.TipoCargas.PMP_POPSAE;
            TipoLayout = Enums.LayoutTypes.POPSAE61;
        }
    }
}