using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class BusquedaTipoPerfilPopsae
    {
        public int IdTipo { get; set;  }
        public string Descripcion { get; set; }
        public bool Activo { get; set;  }
        public DateTime FechaRegistro { get; set;  }
        public int IdPerfil { get; set;  }


    }
}
