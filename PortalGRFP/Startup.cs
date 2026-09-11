using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Practices.EnterpriseLibrary.Data;

[assembly: OwinStartup(typeof(PortalGRFP.Startup))]

namespace PortalGRFP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888

            // Con la versión 6.0, esta línea revivirá y funcionará a la perfección:
            //Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }
    }
}
