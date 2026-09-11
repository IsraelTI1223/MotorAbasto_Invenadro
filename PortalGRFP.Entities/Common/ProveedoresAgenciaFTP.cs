using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ProveedoresAgenciaFTP


    {

        public int Id { get; set; }

        public string Proveedor { get; set; }
        public string Agencia { get; set; }
        public string Tipo_Formato { get; set; }
        public string Nomenclatura_Archivo { get; set; }
        public string URL { get; set; }
        public string Directorio { get; set; }
        public string Directorio_Respaldo { get; set; }
        public string Frecuencia { get; set; }
    }
}
