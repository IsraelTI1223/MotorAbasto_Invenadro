using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ReporteArticulosSucursalModel
    {
        //		 	 	 						
        public int ID_Sucursal { get; set; }
        public string SKU { get; set; }
        public string DESCRIPCION_CORTA { get; set; }
        public string GRUPO { get; set; }
        public string FAMILIA { get; set; }
        public string Estatus { get; set; }
        public string Existencia { get; set; }
        public string Transito { get; set; }
        public string PVD { get; set; }
        public string Invenadro { get; set; }
        public int Capacity { get; set; }
        public string Motivo { get; set; }
        public string TipoProveedor { get; set; }
        public string RotABC { get; set; }
        public string PoliticaDias { get; set; }
        public string FechaActivo { get; set; }
        public string FechaInactivo { get; set; }
        public string piezas_total { get; set; }

    }
}
