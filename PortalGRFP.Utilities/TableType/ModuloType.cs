using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Utilities.TableType
{
    public static class ModuloType
    {
        public static DataTable GetDefinition()
        {
            var dt = new DataTable();

            dt.Columns.Add("IdModulo", typeof(int));
            dt.Columns.Add("Actualizar", typeof(int));
            dt.Columns.Add("Consultar", typeof(int));
            dt.Columns.Add("Cargar", typeof(int));
            dt.Columns.Add("Eliminar", typeof(int));
            dt.Columns.Add("Insertar", typeof(int));
            dt.Columns.Add("Descargar", typeof(int));

            return dt;
        }
    }
}
