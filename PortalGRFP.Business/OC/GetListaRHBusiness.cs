using PortalGRFP.Data.OC;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.OC
{
    public class GetListaRHBusiness
    {
        public readonly ConfiguracionOCData _insertData = new ConfiguracionOCData();

        public int InsertOC(string id_sugerido, string user)
        {
            return _insertData.InsertOC(id_sugerido, user);
        }



        public ResponseList<OCSugerido> GetConsultaOrdenesCompra(string id_sugerido)//int Cadena
        {
            var response = new ResponseList<OCSugerido>();
            try
            {
                response = _insertData.CargaOC(id_sugerido);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }

    }
}
