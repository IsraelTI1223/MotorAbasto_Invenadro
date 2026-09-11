using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public static class CoberturaType
    {
        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("IDFORMA", typeof(int));
            dt.Columns.Add("IT_TIPO", typeof(int));
            dt.Columns.Add("ID_CLASIFICACIONMONTO", typeof(int));
            dt.Columns.Add("MONTODE", typeof(decimal));
            dt.Columns.Add("MONTOHASTA", typeof(decimal));
            dt.Columns.Add("ID_CLASIFICACIONPZA", typeof(int));
            dt.Columns.Add("PIEZASDE", typeof(int));
            dt.Columns.Add("PIEZASHASTA", typeof(int));
            dt.Columns.Add("PORC_PARTICIPACION_GRUPO", typeof(bool));
            dt.Columns.Add("PORC_PARTICIPACION_DIVISION", typeof(bool));
            dt.Columns.Add("DIAS_COBERTURA_MAYORISTA", typeof(decimal));
            dt.Columns.Add("DIAS_COBERTURA_CEDIS", typeof(string));
            dt.Columns.Add("DIAS_COBERTURA_PIECAMION", typeof(string));
            dt.Columns.Add("FRECUENCIA", typeof(int));
            dt.Columns.Add("USUARIO_ACTUALIZA", typeof(int));
            return dt;
        }
    }
}
