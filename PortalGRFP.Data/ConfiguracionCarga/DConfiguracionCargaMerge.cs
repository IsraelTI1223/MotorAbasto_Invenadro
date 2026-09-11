namespace PortalGRFP.Data.ConfiguracionCarga
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Request;
    using PortalGRFP.Entities.Response;
    using System;
    using System.Data;
    public class DConfiguracionCargaMerge
    {
        public Response Execute(Request<ConfiguracionCarga> request)
        {
            var response = new Response();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_GRFP_CTRL_CARGA_MERGE"); 

            db.AddInParameter(command, "@IdCarga", DbType.Int32, request.Parameters.IdCarga);
            db.AddInParameter(command, "@IdTipoCarga", DbType.Int32, request.Parameters.IdTipoCarga);
            db.AddInParameter(command, "@ArchivosPorDia", DbType.Int32, request.Parameters.ArchivosPorDia);
            db.AddInParameter(command, "@HoraInicio", DbType.Time, Convert.ToDateTime(request.Parameters.HoraInicio.ToString()));
            db.AddInParameter(command, "@HoraFin", DbType.Time, Convert.ToDateTime(request.Parameters.HoraFin.ToString()));
            db.AddInParameter(command, "@IdUsuario", DbType.Int32, request.IdUsuario);

            var idCarga = (int)db.ExecuteScalar(command);
            response.Success = idCarga != default;            
            return response;
        }
    }
}
