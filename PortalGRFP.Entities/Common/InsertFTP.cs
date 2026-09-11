using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class InsertFTP
    {
        public int Id { get; set; }
        public int Id_Proveedor { get; set; }
        public int Id_Agencia { get; set; }
        public int Tipo_Formato { get; set; }
        public string URL { get; set; }
        public string Directorio { get; set; }
        public string Directorio_Respaldo { get; set; }
        public string Copia_Respaldo { get; set; }
        public string Elimina_Origen { get; set; }
        public int Frecuencia { get; set; }
        public string Tiempo_Frecuencia { get; set; }
        public string Fecha_Proxima_Ejecucion { get; set; }
        public string Hora_Ini { get; set; }
        public string Hora_Fin { get; set; }
        public string Usuario_FTP { get; set; }
        public string Clave_FTP { get; set; }
        public string Nomenclatura_Archivo { get; set; }
        public string o_usr { get; set; }

        // Nuevos campos
        public string FTP_Activo { get; set; }
        public int Id_RazonSocial { get; set; }
        public string Numero_Frecuencia { get; set; }
        public string Lapso_Frecuencia { get; set; }
        public string Extension_Formato { get; set; }

    }
}
