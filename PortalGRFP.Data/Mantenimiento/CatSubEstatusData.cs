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
    public class CatSubEstatusData
    {
        public List<CatSubEstatusModel> GetCatSubEstData(int flag, int id)
        {
            var response = new List<CatSubEstatusModel>();
            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_MANT_GET_CAT_SUBESTATUS_PERMISO_COMPRA]");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@id", id));

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response.Add(new CatSubEstatusModel
                    {
                        Id = dr.Get<int>("Id"),
                        Nombre = dr.Get<string>("Nombre"),
                        Estatus = dr.Get<int>("Estatus")
                    });
                }
            }
            return response;
        }

        public Response UICatSubEstatusData(CatSubEstatusModel model)
        {
            var response = new Response();
            var id = 0;
            int flag;

            if (model.Nombre == null)
            {
                flag = 3;
                model.Nombre = "delete";
            }
            else
            {
                if (model.Id > 0) flag = 2;
                else flag = 1;
            }

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("[mta].[SP_GRFP_MANT_UI_CAT_SUBTESTATUS_PERMISO_COMPRA]");

            command.Parameters.Add(new SqlParameter("@flag", flag));
            command.Parameters.Add(new SqlParameter("@Id", model.Id));
            command.Parameters.Add(new SqlParameter("@Nombre", model.Nombre));
            command.Parameters.Add(new SqlParameter("@Estatus", model.Estatus));
            command.Parameters.Add(new SqlParameter("@Usuario", model.IdUsuario));

            var dr = db.ExecuteReader(command);
            while (dr.Read())
            {
                id = dr.Get<int>("done");
            }

            if (id == 3)
            {
                response.Message = "Ya existe un SubEstatus Con ese Nombre";
                response.Success = false;
            }
            else if (id == 1)
            {
                response.Message = "Exito al guardar";
                response.Success = true;
            }
            else
            {
                response.Message = "Error al guardar";
                response.Success = false;
            }
            return response;
        }
    }
}
