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

namespace PortalGRFP.Data.Modulos
{
    public class ModuloData
    {
        public ResponseList<Modulo> GetList()
        {
            var response = new ResponseList<Modulo>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_CAT_MODULO_GETLIST");
            //db.AddInParameter(command, "@IdTipoCarga", DbType.Int32, request.Parameters.IdTipoCarga);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToModulo());
            response.Success = response.Result.Any();
            return response;
        }

        public ResponseList<ModuloAccion> GetListByPadre(int IdPadre, int TipoOperacion, int IdPerfil)
        {
            var response = new ResponseList<ModuloAccion>();
            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");
            var command = db.GetStoredProcCommand("SP_GRFP_CAT_MODULO_GETBYPADRE");
            db.AddInParameter(command, "@IdPadre", DbType.Int32, IdPadre);
            db.AddInParameter(command, "@TipoOperacion", DbType.Int32, TipoOperacion);
            db.AddInParameter(command, "@IdPerfil", DbType.Int32, IdPerfil);
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToModuloAccion());
            response.Success = response.Result.Any();
            return response;
        }



    }
}
