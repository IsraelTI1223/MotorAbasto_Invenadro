using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Models
{
    public class PerfilModuloModel
    {
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public int Estatus { get; set; }
        public Modulo ModuloPadre { get; set; }
        public List<PerfilModuloModel> Hijos { get; set; }
        public List<PerfilModuloAccionModel> Acciones { get; set; }
    }
}
