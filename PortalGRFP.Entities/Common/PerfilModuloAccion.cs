using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class PerfilModuloAccion
    {
        public int IdModulo { get; set; }
        public int IdPadre { get; set; }
        public string Modulo { get; set; }
        public int IdAccion { get; set; }
        public string Nombre { get; set; }
        public int Opciones { get; set; }
        public int EstatusAccion { get; set; }
    }
}
