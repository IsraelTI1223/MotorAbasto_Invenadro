using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public static class RankingType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("IdAgencia", typeof(int));
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("Desc_SKU", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Rank_monto", typeof(decimal));
            dt.Columns.Add("Rank_piezas", typeof(decimal));
            dt.Columns.Add("IdUsuario", typeof(int));
            return dt;
        }
    }
}
