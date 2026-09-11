using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using PortalGRFP.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PortalGRFP.Data.DConsultaMTA
{
   public class GetListaConsultarMTAData
    {
        public ResponseList<SincronizacionInput> ObtenerConsultaMTA()//int idCarga
        {
            var response = new ResponseList<SincronizacionInput>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_LOAD_LISTA_INPUT");
            command.CommandTimeout = 0;

           
            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaMTA());
            response.Success = response.Result.Any();

            return response;
        }


        public ResponseList<DetalleInput> ObtenerConsultaDetalleMTA(string id_input)//int idCarga
        {
            var response = new ResponseList<DetalleInput>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.SP_GRFP_LOAD_LISTA_INPUT_DETALLE"); //aqui me quede
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@id_input", id_input));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConsultaDetalleMTA());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<DetalleInputVenta> GetVentaDetalleMTA(string id_input, int user)//int idCarga
        {
            var response = new ResponseList<DetalleInputVenta>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.GRFP_LOAD_INPUT_DETALLE_INTGR"); //aqui me quede
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@id_input", id_input));
            command.Parameters.Add(new SqlParameter("@user", user));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToVentaDetalleMTA());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<DetalleInputInvenadro> GetInvenadroDetalleMTA(string id_input, int user)//int idCarga
        {
            var response = new ResponseList<DetalleInputInvenadro>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.GRFP_LOAD_INPUT_DETALLE_INTGR"); //aqui me quede
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@id_input", id_input));
            command.Parameters.Add(new SqlParameter("@user", user));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToInvenadroDetalleMTA());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<DetalleInputNegado> GetNegadoDetalleMTA(string id_input, int user)//int idCarga
        {
            var response = new ResponseList<DetalleInputNegado>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.GRFP_LOAD_INPUT_DETALLE_INTGR"); //aqui me quede
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@id_input", id_input));
            command.Parameters.Add(new SqlParameter("@user", user));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToNegadoDetalleMTA());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<DetalleInputLista> GetListaDetalleMTA(string id_input, int user)//int idCarga
        {
            var response = new ResponseList<DetalleInputLista>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.GRFP_LOAD_INPUT_DETALLE_INTGR"); //aqui me quede
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@id_input", id_input));
            command.Parameters.Add(new SqlParameter("@user", user));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToListaDetalleMTA());
            response.Success = response.Result.Any();

            return response;
        }

        public ResponseList<DetalleInputTransitt> GetTransitoDetalleMTA(string id_input, int user)//int idCarga
        {
            var response = new ResponseList<DetalleInputTransitt>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("mta.GRFP_LOAD_INPUT_DETALLE_INTGR"); //aqui me quede
            command.CommandTimeout = 0;

            command.Parameters.Add(new SqlParameter("@id_input", id_input));
            command.Parameters.Add(new SqlParameter("@user", user));

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToTransitDetalleMTA());
            response.Success = response.Result.Any();

            return response;
        }


    }
}
