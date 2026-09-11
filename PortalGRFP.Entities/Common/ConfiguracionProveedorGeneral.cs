using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalGRFP.Entities.Common
{


	public class ConfProveedoresModel
    {
        public int Id { get; set; }
        public int IdEmpresaERP { get; set; }
        public string IdProveedorERP { get; set; }
        public string RFC { get; set; }
        public string IdProveedorPos { get; set; }
        public string Nombre { get; set; }
        public bool AplicaAgencia { get; set; }
        public bool AplicaMinimo { get; set; }
        public decimal MinimoMonto { get; set; }
        public int minimoPiezas { get; set; }
        public int DiasVigenciaOC { get; set; }
        public int MinimoSurtido { get; set; }
        public int IdUsuarioAlta { get; set; }
    }


   /* public class ConfiguracionProveedorGeneralModel
    {

        public int Id { get; set; }
        public int IdEmpresaERP { get; set; }
        public int IdProveedorERP { get; set; }
        public int IdProveedorPos { get; set; }

        [Required]
        [Display(Name = "Nombre Proveedor:")]
        public string Nombre { get; set; }

        public bool AplicaAgencia { get; set; }
        public bool AplicaMinimo { get; set; }


        [Display(Name = "Monto Minimo:")]
        //[MaxLength(12)]
        //[MinLength(1)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Solo números")]
        public decimal MinimoMonto { get; set; }

        [Display(Name = "Piezas Minimas:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int minimoPiezas { get; set; }

        [Display(Name = "Dias Vigencia:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int DiasVigenciaOC { get; set; }
        public int IdUsuarioAlta { get; set; }
        public List<SelectListItem> lstProveedores { get; set; }

    }*/

   /* public class ConfiguracionesProveedorModel
    {
        public int Id { get; set; }
        public int IdEmpresaERP { get; set; }
        public int IdProveedorERP { get; set; }
        public int IdProveedorPos { get; set; }
        public string Nombre { get; set; }
        public bool AplicaAgencia { get; set; }
        public bool AplicaMinimo { get; set; }
        public decimal MinimoMonto { get; set; }
        public int minimoPiezas { get; set; }
        public int DiasVigenciaOC { get; set; }
        public int IdUsuarioAlta { get; set; }
    }*/

    //public class busquedaProv
    //   {
    //	public string idproveedor { get; set; }
    //   }

}
