using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class LogInvenadro
    {
        public int Sucursal { get; set; }
        public string SKU { get; set; }

        public string Descripcion { get; set; }

        public int Optimo { get; set; }

        public string Error { get; set; }

        public string Fecha { get; set; }
    }

    public class SalidaInvenadro
    {
        public List<LogInvenadro> Log { get; set; }

        public List<LogInvenadro> Bitacora { get; set; }
    }

}
