using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace PortalGRFP.Entities.Common
{
    public class MantenimientoSucursal
    {
        [DisplayName("Sucursal")]
        public int IdSucursal { get; set; }

        public int Grupo { get; set; }
        [DisplayName("Invenadro")]
        public bool Invenadro { get; set; }
        public bool PVD { get; set; }

        [DisplayName("Regla de Negado")]
        public bool Negados { get; set; }
    }
}
