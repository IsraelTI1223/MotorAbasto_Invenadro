using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Parameters;
using PortalGRFP.Entities.Request;
using PortalGRFP.Entities.Response;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PortalGRFP.Data.Decuentos
{
    public class BulkLoadData
    {
        public Response Execute(Request<UpdLoadDiscoutParameter> request)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_GRFP_TRAN_DESCUENTO_INSERT");

            db.AddInParameter(command, "@IdUsuario", DbType.Int32, request.IdUsuario);
            db.AddInParameter(command, "@IdTipoCarga", DbType.Int32, request.Parameters.IdTipoCarga);
            db.AddInParameter(command, "@NombreArchivo", DbType.String, request.Parameters.NombreArchivo);
            db.AddInParameter(command, "@IdCliente", DbType.Int32, request.Parameters.IdCliente);
            db.AddInParameter(command, "@IdTipoCliente", DbType.Int32, request.Parameters.IdTipoCliente);
            db.AddInParameter(command, "@IdSubCliente", DbType.Int32, request.Parameters.IdSubCliente);
            db.AddInParameter(command, "@Nombre", DbType.String, request.Parameters.Nombre);
            db.AddInParameter(command, "@NombreSubcliente", DbType.String, request.Parameters.NombreSubcliente);
            command.Parameters.Add(new SqlParameter("@DescuentoType", SqlDbType.Structured) { Value = request.Parameters.Discounts });
            command.Parameters.Add(new SqlParameter("@LogDescuentoType", SqlDbType.Structured) { Value = request.Parameters.Errors });
            //var result = db.ExecuteNonQuery(command);
            //response.Success = result != 0;            
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToResponse()).FirstOrDefault();

            return response;
        }
    }
}
