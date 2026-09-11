using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public class EliminarSugeridoType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();
            dt.Columns.Add("IdSugerido", typeof(long));
            dt.Columns.Add("IdSucursal", typeof(int));
            dt.Columns.Add("Proveedor", typeof(string));
            dt.Columns.Add("sku", typeof(string));
            dt.Columns.Add("Usuario", typeof(int));
            return dt;
        }
    }
}
