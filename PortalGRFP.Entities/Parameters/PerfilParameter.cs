using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Parameters
{
    public class PerfilParameter
    {
        public int IdPerfil { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DataTable Modulos { get; set; }
    }
}
