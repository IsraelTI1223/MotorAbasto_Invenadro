using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Data.ConfGeneralesAbastoComProv
{
    public class GetListaConsultarPRVData
    {
        public ResponseList<PorcentajeProv> ObtenerConsultaPRV()//int idCarga
        {
            var response = new ResponseList<PorcentajeProv>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_SELECT_COM_PROV");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaPRV());
            response.Success = response.Result.Any();

            return response;
        }
    }
}
