using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public class SucursalProveedorType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("id_proveedor", typeof(int));
            dt.Columns.Add("id_suc", typeof(int));
            dt.Columns.Add("lead_time", typeof(int));
            dt.Columns.Add("dias_CobAdicional", typeof(decimal));
            dt.Columns.Add("Agencia", typeof(decimal));
            dt.Columns.Add("No_clientProv", typeof(int));
            dt.Columns.Add("lunes",   typeof(int));
            dt.Columns.Add("martes",  typeof(int));
            dt.Columns.Add("miercoles",typeof(int));
            dt.Columns.Add("jueves", typeof(int));
            dt.Columns.Add("viernes", typeof(int));
            dt.Columns.Add("sabado", typeof(int));
            dt.Columns.Add("domingo", typeof(int));
            dt.Columns.Add("o_flag", typeof(string));
            dt.Columns.Add("o_dttm", typeof(DateTime));
            dt.Columns.Add("o_dttc", typeof(DateTime));
            dt.Columns.Add("o_usr", typeof(int));
            return dt;
        }

    }
}
