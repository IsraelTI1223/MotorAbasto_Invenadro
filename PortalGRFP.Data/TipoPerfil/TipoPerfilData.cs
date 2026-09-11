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

namespace PortalGRFP.Data.TipoPerfil
{
    public class TipoPerfilData
    {

        public ResponseList<BusquedaTipoPerfil> GetClientesByTipo(int IdTipo,int opcion)
        {
            var response = new ResponseList<BusquedaTipoPerfil>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_BUSQUEDA_TIPO");
            db.AddInParameter(command, "@IdPerfil", DbType.Int32, IdTipo);
            db.AddInParameter(command, "@Opcion", DbType.Int32, opcion);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.TipoBusqueda()).OrderBy(x=> x.Nombre).ToList();
            response.Success = response.Result.Any();
            return response;
        }

    }
}
