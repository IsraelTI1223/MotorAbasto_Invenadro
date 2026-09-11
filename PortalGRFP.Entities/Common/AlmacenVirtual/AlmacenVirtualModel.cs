using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.AlmacenVirtual
{
    public class AlmacenVirtualModel
    {
        public string Fecha { get; set; }
        public string Contrato { get; set; }
        public string Sku { get; set; }
        public string Descripcion { get; set; }
        public string Laboratorio { get; set; }
        public int Piezas { get; set; }
        public decimal Costo { get; set; }
        public int AlmacenV { get; set; }
        public int user { get; set; }

        public AlmacenVirtualModel()
        {
            this.Fecha = string.Empty;
            this.Contrato = string.Empty;
            this.Sku = string.Empty;
            this.Descripcion = string.Empty;
            this.Laboratorio = string.Empty;
            this.Piezas = 0;
            this.Contrato = string.Empty;
            this.AlmacenV = 0;
            this.user = 0;
        }
    }
}
