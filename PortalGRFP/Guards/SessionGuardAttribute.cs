using PortalGRFP.Controllers;
using PortalGRFP.Entities.Enums;
using PortalGRFP.Extensions;
using PortalGRFP.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace PortalGRFP.Guards
{
    /// <summary>
    /// Atributo para validar permisos
    /// </summary>
    public class SessionGuardAttribute : ActionFilterAttribute
    {
        #region [Propiedades]
        string Controller, Acction, ErrorMessage;
        int ModuloPadre, SubModuloHijo, IdAccion;
        #endregion
        /// <summary>
        /// Constructor comun
        /// </summary>
        /// <param name="controller">Controlador para redirect</param>
        /// <param name="acction">Accion para redirect</param>
        /// <param name="errormessage">Mensaje de error</param>
        /// <param name="moduloPadre">Modulo padre</param>
        /// <param name="subModuloHijo">Sub modulo</param>
        /// <param name="idAccion">Accion que se ejecuta</param>
        public SessionGuardAttribute(string controller = "Home", string acction = "ErrorPermiso",
            string errormessage = "El usuario no tiene los permisos para este módulo",
            int moduloPadre = 0, int subModuloHijo = 0, Acciones idAccion = Acciones.Consultar)
        {
            Controller = controller;
            Acction = acction;
            ModuloPadre = moduloPadre;
            SubModuloHijo = subModuloHijo;
            IdAccion = (int)idAccion;
            ErrorMessage = errormessage;
        }
        /// <summary>
        /// Metodo interceptor de acciones
        /// </summary>
        /// <param name="filterContext">Contexto</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ModuloPadre == 0)
            {
                var info = filterContext.Controller.GetType().GetCustomAttribute<SessionGuardAttribute>(true);
                ModuloPadre = info.ModuloPadre;
            }

            var user = filterContext.Controller.GetUsuario();
            bool flag = filterContext.Controller.IsCanActive(ModuloPadre, SubModuloHijo, (Acciones)IdAccion);
            if (user != null && !flag)
            {
                if (IsJsonResul(filterContext))
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.HttpContext.Response.End();
                }
                filterContext.HttpContext.Session[Resources.SESSION_KEY_PERMISOS_ERROR] = ErrorMessage;
                filterContext.Result = new RedirectResult($"/{Controller}/{Acction}");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
        /// <summary>
        /// Metodo para validar el tipo de peticion
        /// </summary>
        /// <param name="context">Contexto</param>
        /// <returns>Bandera</returns>
        private bool IsJsonResul(ActionExecutingContext context)
        {
            var tem = (context.ActionDescriptor as ReflectedActionDescriptor).MethodInfo;
            return tem.ReturnType.Name.Equals("JsonResult");
        }
    }
}