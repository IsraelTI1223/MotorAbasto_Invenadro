using PortalGRFP.Entities.Models;
using PortalGRFP.Properties;
using System.Web.Mvc;

namespace PortalGRFP.Extensions
{
    public static class SessionStorage
    {
        /// <summary>
        /// Metodo para obtener el usuario en sesion
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <returns>Usuario en sesion</returns>
        public static UsuarioModel GetUsuario(this Controller context) => context.Session[Resources.SESSION_KEY_USER] as UsuarioModel;
        /// <summary>
        /// Metodo para obtener el usuario en sesion
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <returns>Usuario en sesion</returns>
        public static UsuarioModel GetUsuario(this ControllerBase context) => context.ControllerContext.HttpContext.Session[Resources.SESSION_KEY_USER] as UsuarioModel;
        /// <summary>
        /// Metodo para obtener el token de gmail en sesion
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <returns>Token de gmail en sesion</returns>
        public static string GetTokenGmail(this Controller context) => context.Session[Resources.SESSION_KEY_GMAIL_TOKEN] as string;
        /// <summary>
        /// Metodo para obtener mensajes de error por falta de permisos
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <returns>Mensaje de error</returns>
        public static string GetErrorPermisos(this Controller context) => context.Session[Resources.SESSION_KEY_PERMISOS_ERROR] as string;
    }
}