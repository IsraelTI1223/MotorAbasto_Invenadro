using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public static class LogDescuentoType
    {
        public static DataTable GetDefinition()
        {
            var dt = new DataTable();

            dt.Columns.Add("CodigoEAN", typeof(string));
            dt.Columns.Add("Error", typeof(string));

            return dt;
        }
    }
}
