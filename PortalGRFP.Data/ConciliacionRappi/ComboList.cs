using Microsoft.Practices.EnterpriseLibrary.Data;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Common;
using System.Collections.Generic;
using System.Data;

namespace PortalGRFP.Data.ConciliacionRappi
{
    public class ComboList
    {
        public List<ComboGenerico> ComboEmpresa()
        {
            var response = new List<ComboGenerico>();

            //var factory = new DatabaseProviderFactory();
            //var db = factory.Create("Devoluciones");
            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("rappi.sp_cadenas");
            command.CommandTimeout = 0;
            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }

        public List<ComboGenerico> ComboFarmacias(int cadena)
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("rappi.sp_Farmacias");
            db.AddInParameter(command, "@RazonSocial", DbType.Int32, cadena);

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }

        public List<ComboGenerico> ComboIncidencias()
        {
            var response = new List<ComboGenerico>();

            var db = DatabaseFactory.CreateDatabase("dev_GRFP_RCB_DB");
            var command = db.GetStoredProcCommand("rappi.sp_Incidencias");

            var read = db.ExecuteReader(command);
            response = read.Reader(x => x.ToComboGenerico());

            return response;
        }


    }
}
