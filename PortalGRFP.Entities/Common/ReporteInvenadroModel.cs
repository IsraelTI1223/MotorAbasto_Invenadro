using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ReporteInvenadroModel
    {
       
       
        public string Marca { get; set; }
        public int Suc_id { get; set; }
        public string Nombre_Corto { get; set; }
        public string Articulo_id { get; set; }
        public string DescripcionCorta { get; set; }
        public int Optimo { get; set; }
        public string Fecha_optimo { get; set; }
        public string Fecha_Consulta { get; set; }

    }
}
