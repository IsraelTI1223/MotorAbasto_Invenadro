using System;

namespace PortalGRFP.Entities.Common
{
    public class ConfiguracionCarga
    {
        public int IdCarga { get; set; }
        public int IdTipoCarga { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int ArchivosPorDia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
