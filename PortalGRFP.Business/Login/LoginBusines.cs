using PortalGRFP.Data.Login;
using PortalGRFP.Entities.Models;
using PortalGRFP.Utilities.Core.Interceptors;
using PortalGRFP.Utilities.Core.Responses;

namespace PortalGRFP.Business.Login
{
    public class LoginBusines
    {
        private readonly LoginData loginData = new LoginData();
        public ResponseSimple<UsuarioModel> Login(string correo)
        => CoreInterceptor.Trace(loginData.Login, correo);
    }
}