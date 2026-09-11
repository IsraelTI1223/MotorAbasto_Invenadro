using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class CatSubEstatusModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estatus { get; set; }
        public int IdUsuario { get; set; }
    }
}
