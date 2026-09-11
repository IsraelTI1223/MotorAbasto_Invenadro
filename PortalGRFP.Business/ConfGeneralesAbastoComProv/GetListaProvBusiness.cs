using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Data.ConfGeneralesAbastoComProv;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Business.ConfGeneralesAbastoComProv
{
   public class GetListaProvBusiness
    {


        private readonly GetListaConsultarPRVData getListaConsultaPRVData;
        public GetListaProvBusiness()
        {
            getListaConsultaPRVData = new GetListaConsultarPRVData();
        }

        public ResponseList<PorcentajeProv> ObtenerConsultaPRV()

        {
            var response = new ResponseList<PorcentajeProv>();
            try
            {
                response = getListaConsultaPRVData.ObtenerConsultaPRV();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }


        public readonly AltaData _insertData = new AltaData();

        public int InsertData(InsertProveedor model, string user)
        {
            return _insertData.InsertAltaProv(model, user);
        }

        public readonly AltaData _updateData = new AltaData();

        public int UpdateData(PorcentajeProv model, string user)
        {
            return _updateData.UpdateProv(model, user);
        }

        public readonly AltaData _deleteData = new AltaData();

        public int DeleteData(PorcentajeProv model, string user)
        {
            return _deleteData.DeleteProv(model, user);
        }
    }
}
