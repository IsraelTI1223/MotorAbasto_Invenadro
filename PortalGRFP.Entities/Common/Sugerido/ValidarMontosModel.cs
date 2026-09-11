using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Sugerido
{
    public class ValidarMontosModel
    {
        public long IdSugerido { get; set; }
        public int Idsucursal { get; set; }
        public bool Autorizado { get; set; }
    }
}
