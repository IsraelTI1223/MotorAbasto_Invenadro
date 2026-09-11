using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.ConsultaSucursalProveedor
{
    public class ComboList
    {


        public List<ComboGenerico> ComboGrupo()
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_SUCURSAL_PROVEEDOR_GRUPOS");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboSucursalGrupo());

            return response;
        }

        public List<ComboGenerico> ComboIdSucursal(int Id)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_GET_SUCURSAL_PROVEEDOR_GRUPOS_SUCURSAL");
            db.AddInParameter(command, "@Id", DbType.Int32, Id);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }

    }
}
