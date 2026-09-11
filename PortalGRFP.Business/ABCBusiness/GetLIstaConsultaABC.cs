using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Data.ABCData;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Capacity;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Business.ABCBusiness
{
    public class GetLIstaConsultaABC
    {

        private readonly GetListaABCData getListaABCData;
        public GetLIstaConsultaABC()
        {
            getListaABCData = new GetListaABCData();
        }

        public ResponseList<ABC> ObtenerConsultaABC()
        {
            var response = new ResponseList<ABC>();
            try
            {
                response = getListaABCData.ObtenerConsultaMTA();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
        
        public CapacityModel updateABC(int idABC, string clasFinal)
        {
            var response = new CapacityModel();
            try
            {
                response = getListaABCData.updateABC(idABC, clasFinal);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar el update. " + ex.Message;
            }
            return response;
        }
    }
}
