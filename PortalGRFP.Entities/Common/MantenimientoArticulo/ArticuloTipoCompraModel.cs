using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common.MantenimientoArticulo
{
    public class ArticuloTipoCompraModel
    {
        public string ProductId { get; set; }
        public string Sku { get; set; }
        public string FormaSurtirLD { get; set; }
        public string FormaSurtirFR { get; set; }
        public string FormaSurtirSSAE { get; set; }
        public string EanEmpaque { get; set; }
        public string FactorEmpaque { get; set; }
        public string Proveedor { get; set; }
        public string Descripcion { get; set; }

        //      ProductId ,
        //          SKU
        //,FORMA_SURTIR_FR ,FORMA_SURTIR_LD
        //,FORMA_SURTIR_SSAE ,EAN_EMPAQUE
        //,FACTOR_EMPAQUE ,PROVEEDOR ,DESCRIPCION_LARGA
    }
}
