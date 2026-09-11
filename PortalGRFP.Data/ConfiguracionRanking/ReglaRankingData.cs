using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalGRFP.Entities.Common;
using PortalGRFP.Data.Extensions;

namespace PortalGRFP.Data.ConfiguracionRanking
{
    public class ReglaRankingData
    {
        public Response BorrarRanking(int usuario)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_borrar_Ranking_Nadro");
            command.CommandTimeout = 0;
            //command.Parameters.Add(new SqlParameter("@sucursales", SqlDbType.VarChar) { Value = sucursales });
            command.Parameters.Add(new SqlParameter("@idusuario", SqlDbType.Int) { Value = usuario });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }

        public Response CargaRanking(DataTable DtRanking)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("mta.sp_Ranking_CargaMasiva");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_Ranking", SqlDbType.Structured) { Value = DtRanking });

            var exito = db.ExecuteNonQuery(command);
            response.Success = exito != default;

            return response;
        }


        public ResponseList<LogRanking> GetRanking(int accion)//int idCarga
        {
            var response = new ResponseList<LogRanking>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            //string sp = "mta.sp_getTotalizado_Invenadro";
            if (accion == 1)
            {
                string sp = "mta.sp_getLOG_Ranking";
                var command = db.GetStoredProcCommand(sp);
                command.CommandTimeout = 0;
                var read = db.ExecuteReader(command);

                response.Result = read.Reader(x => x.ToLogRanking());
                response.Success = response.Result.Any();
            }

            //command.Parameters.Add(new SqlParameter("@Tipo_Invenadro", SqlDbType.Structured) { Value = DtInvenadro });
            return response;
        }
        public ResponseList<ConteoRanking> ConteoRanking(int accion)
        {
            var response = new ResponseList<ConteoRanking>();
            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            if (accion == 2)
            {
                string sp = "mta.sp_getLOG_Ranking_Conteo";
                var command = db.GetStoredProcCommand(sp);
                command.CommandTimeout = 0;
                var read = db.ExecuteReader(command);

                response.Result = read.Reader(x => x.ToConteoRanking());
                response.Success = response.Result.Any();
            }
            return response;
        }
       

    }
}
