using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.GetSubClientesPopsae
{
   public class SubclientesPopsae
    {

        public ResponseList<BusquedaTipoPerfilPopsae> GetSubClientesByTipo(int IdPerfil)
        {
            var response = new ResponseList<BusquedaTipoPerfilPopsae>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_BUSQUEDA_SUB_CLIENTE_POPSAE");
            db.AddInParameter(command, "@IdPerfil", DbType.Int32, IdPerfil);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.TipoBusquedaPopsae());
            response.Success = response.Result.Any(); 
            return response;
        }

    }
}
