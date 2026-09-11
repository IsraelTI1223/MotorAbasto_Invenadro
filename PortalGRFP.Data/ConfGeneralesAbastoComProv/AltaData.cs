using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;

namespace PortalGRFP.Data.ConfGeneralesAbastoComProv
{
    public class AltaData
    {

        public int InsertAltaProv(InsertProveedor model, string user)
        {
            var id = 0;

            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_INSERT_PROVEEDOR_DTL");
            command.Parameters.Add(new SqlParameter("@Id", model.Id));
            command.Parameters.Add(new SqlParameter("@Proveedor", model.Proveedor));
            command.Parameters.Add(new SqlParameter("@Descr", model.Descr));
            command.Parameters.Add(new SqlParameter("@o_usr", user));
          

            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            return id;



        }

        public int UpdateProv(PorcentajeProv model, string user)
        {
            var id = 0;

            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_UPDATE_PROVEEDOR_DTL");
            command.Parameters.Add(new SqlParameter("@Id", model.Id));
            command.Parameters.Add(new SqlParameter("@Empates", model.Empates));
            command.Parameters.Add(new SqlParameter("@ProteccionCosto", model.ProteccionCosto));
            command.Parameters.Add(new SqlParameter("@PlanCrecimiento", model.PlanCrecimiento));
            command.Parameters.Add(new SqlParameter("@NumProductos", model.NumProductos));


            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            return id;

        }


        public int DeleteProv(PorcentajeProv model, string user)
        {
            var id = 0;

            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");
            var command = db.GetStoredProcCommand("mta.SP_GRFP_CONF_DELETE_PROVEEDOR_DTL");
            command.Parameters.Add(new SqlParameter("@Id", model.Id));
            command.Parameters.Add(new SqlParameter("@o_usr", user));



            var dr = db.ExecuteReader(command);

            response.Success = dr != default;

            return id;

        }



    }
}
