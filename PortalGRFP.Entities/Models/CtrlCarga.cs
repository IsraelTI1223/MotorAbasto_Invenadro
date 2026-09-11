using System;

namespace PortalGRFP.Entities.Models
{
    public class CtrlCarga
    {
        public int ArchivosPorDia { get; set; }
        public DateTime? FechaUltimaCarga { get; set; }
        public TimeSpan HoraFin { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public int NumeroCargas { get; set; }
    }
}