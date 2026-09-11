using PortalGRFP.Data.Acciones;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Business.Acciones
{
    public class AccionBusiness
    {
        private readonly AccionData accionData;

        public AccionBusiness()
        {
            accionData = new AccionData();
        }

        public ResponseList<Accion> GetList()
        {
            var response = new ResponseList<Accion>();
            try
            {
                response = accionData.GetList();
            }
            catch (Exception ex)
            {
                response.Message = "Error al realizar la consulta de información." + ex.Message;
            }
            return response;
        }
    }
}
