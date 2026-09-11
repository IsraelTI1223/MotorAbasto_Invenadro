namespace PortalGRFP.Data.ConfiguracionCarga
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using PortalGRFP.Data.Extensions;
    using PortalGRFP.Entities.Common;
    using PortalGRFP.Entities.Response;
    using System.Data;
    using System.Linq;
    using System.Xml.XPath;

    public class DConfiguracionCargaGetList
    {
        public ResponseList<ConfiguracionCarga> Execute(int idCarga)
        {
            var response = new ResponseList<ConfiguracionCarga>();

            var db = DatabaseFactory.CreateDatabase("DBPORTALRCB");

            var command = db.GetStoredProcCommand("SP_GRFP_CTRL_CARGA_GETLIST");

            db.AddInParameter(command, "@IdCarga", DbType.Int32, idCarga);

            var read = db.ExecuteReader(command);
            response.Result = read.Reader(x => x.ToConfiguracionCarga());
            response.Success = response.Result.Any();

            return response;
        }
    }
}
