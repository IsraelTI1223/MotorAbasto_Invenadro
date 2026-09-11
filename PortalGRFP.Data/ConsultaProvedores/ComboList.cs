using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.ConsultaProvedores
{
   public class ComboList
    {


        public List<ComboProveedor> ComboProveedor()
        {
            var response = new List<ComboProveedor>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONSUL_PROVEEDOR_REPORTE");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboProveedorReporte());

            return response;
        }



        public List<ComboGenerico> ComboIdProv(int Id)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONSUL_PROVEEDOR_AGENCIA_REPORTE");
            db.AddInParameter(command, "@id_proveedor", DbType.Int32, Id);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }
    }
}
