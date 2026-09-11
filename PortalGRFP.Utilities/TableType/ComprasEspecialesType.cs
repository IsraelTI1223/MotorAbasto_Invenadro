using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public class CommprasEspecialesType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("id_tipo", typeof(int));
            dt.Columns.Add("id_proveedor", typeof(int));
            dt.Columns.Add("fecha_oc", typeof(DateTime));
            dt.Columns.Add("id_suc", typeof(int));
            dt.Columns.Add("sku", typeof(string));
            dt.Columns.Add("cantidad", typeof(int));
            dt.Columns.Add("lineas", typeof(int));
            dt.Columns.Add("nombreArchivo", typeof(string));
            dt.Columns.Add("costo", typeof(decimal));
            dt.Columns.Add("o_usr", typeof(int));
            return dt;
        }

    }
}
