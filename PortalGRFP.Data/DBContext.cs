using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PortalGRFP.Data
{
    public abstract class DBContext
    {
        protected Database Context { get { return DatabaseFactory.CreateDatabase("DBPORTALRCB"); } }
        //protected Database Context { get { return DatabaseFactory.CreateDatabase("site_settings"); } }

    }
}
