using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class SugeridoLogErrorModel
    {
        public string FechaCarga { get; set; }
        public string Fecha_Validacion { get; set; }
        public string Motivo_Negado { get; set; }
        public string Sucursal { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public int Piezas_rechazadas { get; set; }
        public string Incidencia { get; set; }

    }
}
