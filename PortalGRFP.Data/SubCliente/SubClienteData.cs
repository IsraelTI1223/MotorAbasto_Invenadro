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

namespace PortalGRFP.Data.SubCliente
{
    public class SubClienteData
    {

        public ResponseList<BusquedaTipoPerfil> GetSubClientesBy(int IdCliente)
        {
            var response = new ResponseList<BusquedaTipoPerfil>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_BUSQUEDA_SUBCLIENTE");
            db.AddInParameter(command, "@IdCliente", DbType.Int32, IdCliente);        
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.TipoBusqueda()).OrderBy(x => x.SubCliente).ToList();
            response.Success = response.Result.Any();
            return response;
        }
    }
}
