using PortalGRFP.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Parameters
{
    public class UpdLoadDiscoutParameter
    {
        public int IdTipoCarga { get; set; }
        public string NombreArchivo { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoCliente { get; set; }     
        public int IdSubCliente { get; set; }
        public string Nombre { get; set; }
        public string NombreSubcliente { get; set;  }
        public DataTable Discounts { get; set; }
        public DataTable Errors { get; set; }
    }
}
