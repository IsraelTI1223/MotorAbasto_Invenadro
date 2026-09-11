using System.Collections.Generic;

namespace PortalGRFP.Entities.Models
{
    public class ModuloModel : ModuloBaseModel
    {
        public int IdModulo { get; set; }
        public int IdModuloPadre { get; set; }
        public string Modulo { get; set; }
        public List<SubModuloModel> SubModulos { get; set; }
        public List<ModuloModel> SubModuloHijos { get; set; }
        public Dictionary<int, string> Acciones { get; set; }
    }
}