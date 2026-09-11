using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
   public  class ProveedoresAgenciaFTPCompleto
    {
        public int Id { get; set; }
        public string Proveedor { get; set; }
        public string Agencia { get; set; }
        public string Tipo_Formato { get; set; }
        public string URL { get; set; }
        public string Directorio { get; set; }
        public string Directorio_Respaldo { get; set; }
        public string Copia_Respaldo { get; set; }
        public string Elimina_Origen { get; set; }
        public string Frecuencia { get; set; }
        public string Tiempo_Frecuencia { get; set; }
        public string Fecha_Proxima_Ejecucion { get; set; }
        public string Hora_Ini { get; set; }
        public string Hora_Fin { get; set; }

        [Required]
        public string Usuario_FTP { get; set; }
        public string Clave_FTP { get; set; }
        public string Nomenclatura_Archivo { get; set; }
    }
}
