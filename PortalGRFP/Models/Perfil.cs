using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalGRFP.Models
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}