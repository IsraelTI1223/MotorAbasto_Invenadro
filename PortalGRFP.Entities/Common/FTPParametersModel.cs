using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class FTPParametersModel
    {   
            public int Id { get; set; }
            public int IdProveedor { get; set; }
            public int IdAgencia { get; set; }
            public int TipoFormato { get; set; }
            public string URL { get; set; }
            public int Puerto { get; set; }
            public string Directorio { get; set; }
            public string DirectorioRespaldo { get; set; }
            public string CopiaRespaldo { get; set; }
            public string EliminaOrigen { get; set; }
            public int Frecuencia { get; set; }
            public string TiempoFrecuencia { get; set; }
            public string FechaProximaEjecucion { get; set; }
            public string HoraIni { get; set; }
            public string HoraFin { get; set; }
            public string UsuarioFTP { get; set; }
            public string ClaveFTP { get; set; }
            public string NomenclaturaArchivo { get; set; }
            public string FTPActivo { get; set; }
            public int IdRazonSocial { get; set; }
            public int NumeroFrecuencia { get; set; }
            public string LapsoFrecuencia { get; set; }
            public string ExtensionFormato { get; set; }

    }
}
