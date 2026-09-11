using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class CtrlPerfil
    {
        public int IdCtrlPerfil { get; set; }
        public int IdPerfil { get; set; }
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public bool Registra { get; set; }
        public bool Actualiza { get; set; }
        public bool Elimina { get; set; }
        public bool Consulta { get; set; }
        public bool Activo { get; set; }

    }
}
