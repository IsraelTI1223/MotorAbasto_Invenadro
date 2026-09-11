using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PortalGRFP.Data.ConfGeneralesAbastoComProv
{
    public class ComboList
    {

        public List<ComboGenericoProveedorCompra> ComboProveedor()
        {
            var response = new List<ComboGenericoProveedorCompra>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboProveedor());

            return response;
        }


        public List<ComboGenerico> ComboIdProv(int Id)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_ID");
            db.AddInParameter(command, "@Id", DbType.Int32, Id);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }

        public List<ComboGenerico> ComboDescrProv(int Id)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_PROVEEDOR_DESCR");
            db.AddInParameter(command, "@Id", DbType.Int32, Id);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }


    }
}
