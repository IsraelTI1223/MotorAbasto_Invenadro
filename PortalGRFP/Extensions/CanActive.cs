using PortalGRFP.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalGRFP.Extensions
{
    /// <summary>
    /// Extension para validar permisos
    /// </summary>
    public static class CanActive
    {
        /// <summary>
        /// Metodo extendido, para obtener permisos en acciones concretas
        /// </summary>
        /// <param name="context">Controlador</param>
        /// <param name="moduloPadre">Modulo raiz</param>
        /// <param name="subModulo">Sub modulo contenedor del permiso (Accion)</param>
        /// <param name="idAcction">Accion a ejecutar</param>
        /// <returns>Bandera para validar si existe el permiso o no</returns>
        public static bool IsCanActive(this ControllerBase context, int moduloPadre, int subModulo, Acciones idAcction)
        {
            List<string> smo = new List<string>();
            var permisos = context.GetUsuario();
            bool flag = moduloPadre > 0;
            if (permisos != null)
            {
                permisos.Guards.TryGetValue(moduloPadre, out smo);
                flag = smo != null;
                if (smo != null && smo.Count > 0 && subModulo > 0)
                {
                    var pm = smo.Select(x => x.Split(':'))
                        .Where(f => f[0].Equals(subModulo.ToString()))
                        .Select(y => y[1].Split(';'))
                        .Where(p => p[1].Equals(((int)idAcction).ToString()))
                        .FirstOrDefault();
                    flag = (pm != null);
                }                
            }
            return flag;
        }
    }
}