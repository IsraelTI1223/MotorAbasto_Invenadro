using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities
{
    public static class AgenciaType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("PROVEEDOR_ID", typeof(int));
            dt.Columns.Add("AGENCIA_ID", typeof(int));
            dt.Columns.Add("OCOMPRA_AUTOMATICA", typeof(bool));
            dt.Columns.Add("FACTURA_AUTOMATICA", typeof(bool));
            dt.Columns.Add("CATALOGO_AUTOMATICO", typeof(bool));
            dt.Columns.Add("PERMITE_REMISIONES", typeof(bool));
            dt.Columns.Add("RESPUESTA_FALTANTE", typeof(bool));
            dt.Columns.Add("USUARIO", typeof(int));

            return dt;
        }
    }
}
