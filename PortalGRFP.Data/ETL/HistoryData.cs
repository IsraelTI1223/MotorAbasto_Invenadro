using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using System.Collections.Generic;
using System.Data;

namespace PortalGRFP.Data.ETL
{
    public class HistoryData : DBContext
    {
        public List<DescuentoHdr> GetHistory(int idTipoCarga, int idUsuario)
        {
            var command = Context.GetStoredProcCommand("SP_GRFP_TRAN_LAYOUT_HISTORY");

            Context.AddInParameter(command, "@IdTipoCarga", DbType.Int32, idTipoCarga);
            Context.AddInParameter(command, "@IdUsuario", DbType.Int32, idUsuario);

            var read = Context.ExecuteReader(command);

            return read.Reader(x => x.ToDescuentoHdr());
        }
    }
}