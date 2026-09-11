using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Common.Capacity;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Data.ABCData
{
    public class GetListaABCData
    {
        public ResponseList<ABC> ObtenerConsultaMTA()//int idCarga
        {
            var response = new ResponseList<ABC>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.sp_GRFP_LOAD_ABC_EQ");
            command.CommandTimeout = 0;


            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaABC());
            response.Success = response.Result.Any();

            return response;
        }

        public CapacityModel updateABC(int idABC, string clasFinal)
        {
            var response = new CapacityModel();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.sp_GRFP_LOAD_ABC_EQ");
            command.Parameters.Add(new SqlParameter("@idABC", idABC));
            command.Parameters.Add(new SqlParameter("@clasFinal", clasFinal));

            command.CommandTimeout = 0;

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Success = dr.Get<bool>("Success");
                    response.Message = dr.Get<string>("Message");
                }
            }

            return response;
        }
    }
}
