using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public static class ExclusionesType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("sku", typeof(string));
            dt.Columns.Add("eliminar", typeof(bool));
            dt.Columns.Add("agregar", typeof(bool));
            dt.Columns.Add("idProveedor", typeof(int));
            dt.Columns.Add("usuario", typeof(int));
            return dt;
        }

    }
}
