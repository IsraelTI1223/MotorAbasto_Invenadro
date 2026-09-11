using PortalGRFP.Data.ETL;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Utilities.Core.Interceptors;
using PortalGRFP.Utilities.Core.Responses;
using System.Collections.Generic;

namespace PortalGRFP.Business.ETL
{
    public class HistoryBusiness
    {
        private readonly HistoryData historyData = new HistoryData();

        public ResponseSimple<List<DescuentoHdr>> GetHistory(int idUsuario, TipoCargas tipoCarga)
        {
            var request = new HistoryRequest { IdTipoCarga = (int)tipoCarga, IdUsuario = idUsuario };
            return CoreInterceptor.Trace(TraceProcess, request);
        }
        private List<DescuentoHdr> TraceProcess(HistoryRequest request)
        {
            return historyData.GetHistory(request.IdTipoCarga, request.IdUsuario);
        }
    }

    class HistoryRequest
    {
        public int IdTipoCarga { get; set; }
        public int IdUsuario { get; set; }
    }
}