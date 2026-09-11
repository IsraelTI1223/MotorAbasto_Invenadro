using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities
{
    public class ValidaSugeridoBean
    {
        public int IdGrupo { get; set; }
        public int IdSucursal { get; set; }
        public int IdProveedor { get; set; }
        public bool Negados { get; set; }
        public bool CompraEspecial { get; set; }
        public bool ResurtidoNatural { get; set; }
        public bool Invenadro { get; set; }
        public bool EspecialesFarmacia { get; set; }
        public bool PieCamionMayorista { get; set; }
    }
}
