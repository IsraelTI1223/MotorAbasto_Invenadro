namespace PortalGRFP.Data.Decuentos
{
    using Entities.Response;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using PortalGRFP.Data.Extensions;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Parameters;
    using PortalGRFP.Entities.Request;
    using System.Data;
    using System.Linq;

    public class DDiscountHdrGetList
    {
        public ResponseList<DescuentoHdr> Execute(Request<DiscountGetListParameter> request)
        {
            var response = new ResponseList<DescuentoHdr>();

            var factory = new DatabaseProviderFactory();
            var db = factory.Create("DBPORTALRCB");


            var command = db.GetStoredProcCommand("SP_GRFP_TRAN_DESCUENTO_HDR_GETLIST");

            db.AddInParameter(command, "@IdTipoCarga", DbType.Int32, request.Parameters.IdTipoCarga);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToDescuentoHdr());
            response.Success = response.Result.Any();

            return response;
        }
    }
}
