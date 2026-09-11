using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PortalGRFP.Entities.Common
{
    public class NegadosViewModel
    {
        public int IdNegado { get; set; }

        [Required]
        [Display(Name = "No. de Piezas por Articulo:")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int PiezasXArticulo { get; set; }
        
        [Required]
        [Display(Name = "Costo Articulo:")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números")]
        public decimal Costo_Articulo { get; set; }

        [Required]
        [Display(Name = "Total piezas por Farmacia:")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida, Solo enteros, Valor maximo 120")]
        public int PiezasXFarmacia { get; set; }

        [Required]
        [Display(Name = "Importe de Negados por día:")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números")]
        public decimal Imp_Negados_XDia { get; set; }
        
        [Required]
        public string Invenadro { get; set; }
        
        [Required]
        public string Resto_Productos { get; set; }

        [Required]
        [Display(Name = "Mayorista:")]
        [Range(0, 100, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int Dias_Vigencia_Mayorista { get; set; }

        [Required]
        [Display(Name = "Cedis:")]
        [Range(0, 100, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int Dias_Vigencia_Cedis { get; set; }

        [Required]
        [Display(Name = "Pie de Camión:")]
        [Range(0, 100, ErrorMessage = "Cantidad inválida, Solo enteros")]
        public int Dias_Vigencia_Pie_Camion { get; set; }

        [Required]
        [Display(Name = "Ranking Montos:")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números")]
        public decimal Rank_montos { get; set; }

        [Required]
        [Display(Name = "Ranking Piezas:")]
        [Range(0, 1000000, ErrorMessage = "Cantidad inválida, Solo enteros")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Solo números")]
        public Int32 Rank_piezas { get; set; }

        [Display(Name = "Ranking Nadro:")]
        public bool chkRanking { get; set; }

    }
}
