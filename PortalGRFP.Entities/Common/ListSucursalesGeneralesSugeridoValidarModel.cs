using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ListSucursalesGeneralesSugeridoValidarModel
    {
        public class SucursalesSugerido
        {

            public string ID { get; set; }
            public string Valor { get; set; }

        }


        public class ListSucursales
        {
            public List<SucursalesSugerido> Sucursales { get; set; }

        }

    }
}
