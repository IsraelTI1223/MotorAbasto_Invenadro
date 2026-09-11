using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalGRFP.Models
{
    public class Modulo
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        //public bool Registra { get; set; }
        //public bool Actualiza { get; set; }
        //public bool Elimina { get; set; }
        //public bool Consulta { get; set; }
        public bool Actualizar { get; set; }
        public bool Consultar { get; set; }
        public bool Cargar { get; set; }
        public bool Eliminar { get; set; }
        public bool Insertar { get; set; }
        public bool Descargar { get; set; }
        public int Opciones { get; set; }

    }
}