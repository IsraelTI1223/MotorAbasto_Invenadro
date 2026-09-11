using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ModuloAccion
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        //public string Componente { get; set; }
        //public string IconoBase { get; set; }
        public bool Actualizar { get; set; }
        public bool Consultar { get; set; }
        public bool Cargar { get; set; }
        public bool Eliminar { get; set; }
        public bool Insertar { get; set; }
        public bool Descargar { get; set; }
        public int Opciones { get; set; }
    }
}
