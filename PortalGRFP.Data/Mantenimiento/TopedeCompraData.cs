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

namespace PortalGRFP.Data.Mantenimiento
{
    public class TopedeCompraData
    {
        public Response CargaExcepcionesData(DataTable DtMontos)//int idCarga
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_CONF_LOAD_TOPE_COMPRA]");
            command.CommandTimeout = 0;
            command.Parameters.Add(new SqlParameter("@Tipo_TopedeCompra", SqlDbType.Structured) { Value = DtMontos });

            int id = 0;

            var dr = db.ExecuteReader(command);
            while (dr.Read())
            {
                id = dr.Get<int>("done");
            }

            if (id == 1)
            {
                response.Message = "Proceso realizado con éxito";
                response.Success = true;
            }
            else
            {
                response.Message = "Error en el proceso de carga";
                response.Success = false;
            }

            return response;
        }

        public ResponseList<TopeDeCompraModel> ConsultaTopeData()
        {
            var response = new ResponseList<TopeDeCompraModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_Consulta_TOPE_COMPRA_Rechazo]");


            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaTopeRec());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
                response.Success = false;
            }

            return response;
        }
        public ResponseList<TopeDeCompraModel> ExcelTopeData()
        {
            var response = new ResponseList<TopeDeCompraModel>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");

            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_Consulta_TOPE_COMPRA]");

            command.CommandTimeout = 0;

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaTope());
            response.Success = response.Result.Any();
            if (response.Result.Count == 0)
            {
                response.Message = "No hay registros por mostrar";
                response.Success = false;
            }

            return response;
        }
    }
}
