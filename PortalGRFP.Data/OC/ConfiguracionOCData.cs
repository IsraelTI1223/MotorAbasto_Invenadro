using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Data.OC
{
   public class ConfiguracionOCData
    {
        public int InsertOC(string id_sugerido, string user)
        {
            var id = 0;

            var response = new Response();

            if(id_sugerido == null)
            {
                id_sugerido = "0";
            }
            

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_SUGERIDO_OC_ASIGNACION");

            command.Parameters.Add(new SqlParameter("@id_sugerido", id_sugerido));
            command.Parameters.Add(new SqlParameter("@o_usr", user));


            var dr = db.ExecuteReader(command);


            response.Success = dr != default;

            return id;



        }



        public ResponseList<OCSugerido> CargaOC(string id_sugerido)//int Cadena
        {


            var response = new ResponseList<OCSugerido>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_SUGERIDO_OC_ASIGNACION_LISTA");

            command.Parameters.Add(new SqlParameter("@id_sugerido", id_sugerido));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToResumenCargaOC());
            response.Success = response.Result.Any();

            return response;
        }




    }
}
