using PortalGRFP.Data.Mantenimiento;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.Mantenimiento
{
    public class CatSubEstatusBLL
    {
        private readonly CatSubEstatusData SubData = new CatSubEstatusData();

        public List<CatSubEstatusModel> GetCatalogoSubEstBLL(int flag, int id)
        {
            var listUsrs = SubData.GetCatSubEstData(flag, id).ToList();

            return listUsrs;
        }

        public Response UICatSubEstatusBLL(CatSubEstatusModel model)
        {
            var response = new Response();

            try
            {
                response = SubData.UICatSubEstatusData(model);

            }
            catch (Exception e)
            {

                response.Message = e.Message.ToString();
                response.Success = false;

                Console.WriteLine(e.Message.ToString());

            }
            return response;
        }
    }
}
