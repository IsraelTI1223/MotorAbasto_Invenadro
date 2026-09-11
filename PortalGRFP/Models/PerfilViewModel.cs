using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalGRFP.Models
{
    public class PerfilViewModel
    {
        public int IdPerfil { get; set; }
        [Required]
        [Display(Name ="Nombre: ")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public bool Activo { get; set; }
        public string ModulosSeleccionados { get; set; }
        public List<Models.Modulo> Modulos { get; set; }
    }
}