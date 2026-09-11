using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.Sugerido
{
    public class GenerarOrdenModel
    {
        public long IdSugerido { get; set; }
        public int IdProveedor { get; set; }
        public int IdGrupo { get; set; }
    }
    
    public class EliminarOrdenModel
    {
        public long IdSugerido { get; set; }
        public int Orden_Compra { get; set; }
        public int Id_Sucursal { get; set; }
        public int IdProveedor { get; set; }
        
    }

    public class ArchivoModel
    {
        public string archivo { get; set; }
        public string Rw { get; set; }
        public string Registro { get; set; }

    }

}
