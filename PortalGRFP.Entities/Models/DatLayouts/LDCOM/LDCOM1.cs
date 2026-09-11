using PortalGRFP.Entities.Attributes;
using System;

namespace PortalGRFP.Entities.Models.DatLayouts.LDCOM
{
    public class LDCOM1 : LayoutBase
    {
        [DatLayout(0, 13)]
        public string SKU { get; set; }
        [DatLayout(13, 14)]
        public decimal PMP { get; set; }
        [DatLayout(27, 9)]
        public decimal IVA { get; set; }
        [DatLayout(36, 13)]
        public DateTime? Fecha { get; set; }

        public LDCOM1() {
            TipoCargas = Enums.TipoCargas.PMP_LDCOM;
            TipoLayout = Enums.LayoutTypes.LDCOM49;
        }
    }
}