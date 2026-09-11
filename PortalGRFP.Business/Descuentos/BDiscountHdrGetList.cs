namespace PortalGRFP.Business.Descuentos
{
    using PortalGRFP.Data.Decuentos;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Parameters;
    using PortalGRFP.Entities.Request;
    using PortalGRFP.Entities.Response;
    using System;

    public class BDiscountHdrGetList
    {
        private readonly DDiscountHdrGetList discountHdrGetList;

        public BDiscountHdrGetList()
        {
            discountHdrGetList = new DDiscountHdrGetList();
        }

        public ResponseList<DescuentoHdr> Execute(Request<DiscountGetListParameter> request)
        {
            var response = new ResponseList<DescuentoHdr>();
            try
            {
                response = discountHdrGetList.Execute(request);
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información. " + ex.Message;
            }
            return response;
        }
    }
}
