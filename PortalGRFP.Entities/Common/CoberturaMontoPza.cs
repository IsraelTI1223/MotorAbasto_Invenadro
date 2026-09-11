using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class CoberturaMontoPza
    {
        public int Forma { get; set; }
        public int Tipo { get; set; }

        public int ClasficacionMonto { get; set; }

        public decimal MontoDe { get; set; }
        public decimal MontoHasta { get; set; }

        public int ClasficacionPieza { get; set; }

        public int PiezasDe { get; set; }
        public int PiezasHasta { get; set; }
        public bool ParticipacionGrupo { get; set; }

        public bool ParticipacionDivision { get; set; }

        public int DiasCoberturaMay { get; set; }

        public int DiasCoberturaCed { get; set; }

        public int DiasCoberturaPieCam { get; set; }
        public int Periodicidad { get; set; }
    }

    public class CoberturaMontoPzaExt : CoberturaMontoPza
    {
        public string Forma_Desc { get; set; }
        public string Tipo_Desc { get; set; }

        public string LetraMonto { get; set; }

        public string LetraPza { get; set; }

        public int IdGrupo { get; set; }

        public int IdDivision { get; set; }
    }
}
