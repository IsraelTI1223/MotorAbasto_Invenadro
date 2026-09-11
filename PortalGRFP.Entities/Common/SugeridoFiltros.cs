using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class SugeridoFiltros
    {
        public List<ComboGenerico> Grupo { get; set; }
        public List<ComboGenerico> Sucursales { get; set; }
        public List<ComboGenerico> Proveedor { get; set; }
        public List<ComboGenerico> ReglasSugerido { get; set; }
    }

}
