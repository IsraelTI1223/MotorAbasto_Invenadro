using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Sugerido
{
    public class ListaSugeridoModel
    {
        public string IDSugerido { get; set; }
        public string FechaCalculo { get; set; }
        public string Estatus { get; set; }

        public bool Negado { get; set; }
    }
}
