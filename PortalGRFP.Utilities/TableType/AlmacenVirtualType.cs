using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public class AlmacenVirtualType
    {
        public static DataTable AlmacenV()
        {
            var dt = new DataTable();

            dt.Columns.Add("fecha", typeof(DateTime));
            dt.Columns.Add("Contrato", typeof(string));
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("laboratorio", typeof(string));
            dt.Columns.Add("piezas", typeof(decimal));
            dt.Columns.Add("costo", typeof(decimal));
            dt.Columns.Add("id_almacen", typeof(int));
            dt.Columns.Add("o_user", typeof(int));
            return dt;
        }

    }
}
