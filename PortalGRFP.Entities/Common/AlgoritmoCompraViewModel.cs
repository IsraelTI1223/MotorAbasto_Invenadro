using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PortalGRFP.Entities.Common
{
    public class AlgoritmoCompraViewModel
    {

        public int Id { get; set; }

        //[Required]
        [Display(Name = "Minimo:")]
        //[Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int MinPorcentajeVariacionPedidoSucursal { get; set; }

        [Required]
        [Display(Name = "Máximo:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int MaxPorcentajeVariacionPedidoSucursal { get; set; }

        [Required]
        [Display(Name = "Existencia:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int ExistenciaCedisPermisoMayorista { get; set; }
        [Required]
        [Display(Name = "%Redondeo de Empaque sugerido:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int RedondeoEmpaque { get; set; }

        [Required]
        [Display(Name = "%Redondeo Emp Existencia Invenadro:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int RedondeoEmpInv { get; set; }

        [Required]
        [Display(Name = "%Redondeo Emp Existencia Resurtido:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int RedondeoEmpRes { get; set; }

        //[Required]
        [Display(Name = "Fracción Redondeo:                ")]
        //[Range(decimal.MinValue, decimal.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public string FraccionRedondeo { get; set; }

        [Required]
        [Display(Name = "Días de Cálculo:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int DiasCalculo { get; set; }

        [Required]
        [Display(Name = "Semana 1:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PonderacionSemana1 { get; set; }

        [Required]
        [Display(Name = "Semana 2:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PonderacionSemana2 { get; set; }

        [Required]
        [Display(Name = "Semana 3:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PonderacionSemana3 { get; set; }

        [Required]
        [Display(Name = "Semana 4:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PonderacionSemana4 { get; set; }


        [Range(100, 100, ErrorMessage = "Porcentaje Inválido, la suma debe ser igual a 100%")]
        public int? TotalPonderacion => PonderacionSemana1 + PonderacionSemana2 + PonderacionSemana3 + PonderacionSemana4;

        [Display(Name = "Picos Venta:")]
        public bool PicosVenta { get; set; }

        [Required]
        [Display(Name = "No. Desviaciones:")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int Desviaciones { get; set; }


        [Display(Name = "Almacèn Virtual:")]
        public bool AlmacenVirtualN { get; set; }

    }
}
