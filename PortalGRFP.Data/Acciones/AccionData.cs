using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System.Linq;

namespace PortalGRFP.Data.Acciones
{

    public class AccionData
    {
        public ResponseList<Accion> GetList()
        {
            var response = new ResponseList<Accion>();
            //var factory = new DatabaseProviderFactory();
            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_CAT_ACCION_GETLIST");
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToAccion());
            response.Success = response.Result.Any();
            return response;
        }
    }
}
