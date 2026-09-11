using System.Collections.Generic;

namespace PortalGRFP.Entities.Models
{
    public class SubModuloModel : ModuloBaseModel
    {
        public int IdSubModulo { get; set; }
        public string SubModulo { get; set; }
        public Dictionary<int, string> Acciones { get; set; }
    }
}