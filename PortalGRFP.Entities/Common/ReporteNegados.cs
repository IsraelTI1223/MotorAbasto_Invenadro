using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ReporteNegados
    {
        public string FechaCarga { get; set; }
        public string FechaValidacion {get;set;}
        public string MotivoNegado    {get;set;}
        public string IdSucursal      {get;set;}
        public string NumSucursal     {get;set;}
        public string Nombre          {get;set;}
        public string SKU             {get;set;}
        public string DESCRIPCION     {get;set;}
        public int CantidadAceptada   {get;set;}
        public int CantidadNegada     {get;set;}
        public string Incidencia { get; set; }
    }
}
