namespace PortalGRFP.Filters
{
    using PortalGRFP.Controllers;
    using PortalGRFP.Entities.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class SessionFilter : ActionFilterAttribute
    {
        private UsuarioModel usuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                usuario = (UsuarioModel)HttpContext.Current.Session[Properties.Resources.SESSION_KEY_USER];
                if(usuario == null)
                {
                    if(filterContext.Controller is AccountController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Account/Login");
                    }
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}