using PortalGRFP.Business.Login;
using PortalGRFP.Business.Mantenimiento;
using PortalGRFP.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using System.Web;
using System.Web.Security;


namespace PortalGRFP.Controllers
{
    public class AccountController : Controller
    {
        #region [Vistas]
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }      

        [HttpGet]
        public ActionResult Login(string message)
        {
            return View(model: message);
        }
        #endregion

        #region [ William Gordillo Palomera]
        [HttpGet]
        public ActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult LoginGmail(string correo)
        {
            if (string.IsNullOrEmpty(correo))
            {
                return Redirect(this.Oauth2(correo.Trim()));
            }
            else
            {
                var res = new LoginBusines().Login(correo.Trim());
                if (res.Result != null)
                {
                    return Redirect(this.Oauth2(correo.Trim()));
                }
                else
                {
                    return RedirectToAction("Login", "Account", new { message = res.Messages.FirstOrDefault() });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> SaveGoogleUser(string code)
        {
            var usurario = await this.TokenAsync(code);
            if (usurario.Success)
            {
                Session["Grupos_Usuario"] = new GruposBLL().GetGrupos_Usuario(4).Result;//subimos a session toda la tabla de grupos usuario

                return RedirectToAction("Index", "Home");
            }
            else {
                return RedirectToAction("Login", "Account", new { message = usurario.Messages.FirstOrDefault() });
            }
        }
        #endregion

        #region Login with Microsoft Graph
        public void SignIn()
        {
            Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);

            //if (!Request.IsAuthenticated)
            //{
            //    // Signal OWIN to send an authorization request to Azure
            //    Request.GetOwinContext().Authentication.Challenge(
            //        new AuthenticationProperties { RedirectUri = "/" },
            //        OpenIdConnectAuthenticationDefaults.AuthenticationType);
            //}
            //else
            //{
            //    Request.GetOwinContext().Authentication.Challenge(
            //        new AuthenticationProperties { RedirectUri = "/" },
            //        OpenIdConnectAuthenticationDefaults.AuthenticationType);
            //}
        }

        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();

            if (Request.IsAuthenticated)
            {
                Request.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}