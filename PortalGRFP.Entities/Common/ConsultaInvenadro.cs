using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ConsultaInvenadro
    {
        public string Sucursal { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public string Grupo { get; set; }
        public string Familia { get; set; }
        public int Existencia { get; set; }
        public int Invenadro { get; set; }
    }
}
