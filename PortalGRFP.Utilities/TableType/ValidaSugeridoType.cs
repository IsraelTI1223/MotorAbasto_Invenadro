using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public class ValidaSugeridoType
    {

        public static DataTable Definicion()
        {
            var dt = new DataTable();

            dt.Columns.Add("idGrupo", typeof(int));
            dt.Columns.Add("idSucursal", typeof(int));
            dt.Columns.Add("idProveedor", typeof(int));
            dt.Columns.Add("Negados", typeof(bool));
            dt.Columns.Add("CompraEspecial", typeof(bool));
            dt.Columns.Add("ResurtidoNatural", typeof(bool));
            dt.Columns.Add("Invenadro", typeof(bool));
            dt.Columns.Add("EspecialesFarmacia", typeof(bool));
            dt.Columns.Add("PieCamionMayorista", typeof(bool));
            dt.Columns.Add("usuario", typeof(int));
            return dt;
        }


    }
}
