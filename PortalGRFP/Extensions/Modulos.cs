using PortalGRFP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalGRFP.Extensions
{
    public static class Modulos
    {
        private static Dictionary<int, string> _acciones;

        public static Dictionary<int, string> GetActions(this List<ModuloModel> modulos, int idModulo)
        {
            if (modulos != null)
            {
                getActions(modulos, idModulo);
            }
            return _acciones;
        }

        private static void getActions(List<ModuloModel> modulos, int idModulo)
        {
            foreach (var m in modulos)
            {
                if (m.IdModulo == idModulo)
                {
                    _acciones = m.Acciones;
                    break;
                }
                else
                {
                    if (m.SubModuloHijos != null)
                    {
                        getActions(m.SubModuloHijos, idModulo);
                    }
                }
            }

        }
    }
}