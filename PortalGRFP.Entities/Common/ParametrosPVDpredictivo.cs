using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    public class ParametrosPVDpredictivo
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "% De Crecimiento: ")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PorcCrecimiento { get; set; }

        [Required]
        [Display(Name = "% De Decremento: ")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PorcDecremento { get; set; }

        [Required]
        [Display(Name = "PVD Histórico: ")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PVDHistorico { get; set; }

        [Required]
        [Display(Name = "PVD Actual: ")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PVDActual { get; set; }
    }
}
