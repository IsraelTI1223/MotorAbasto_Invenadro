using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Common
{
    /// <summary>
    /// Modelo para el reporte de inventario automático de Invenadro
    /// </summary>
    public class ReporteInvenadroAutModel
    {
        /// <summary>
        /// Identificador de la sucursal
        /// </summary>
        public int IdSucursal { get; set; }

        /// <summary>
        /// Nombre de la sucursal
        /// </summary>
        public string Sucursal { get; set; }

        /// <summary>
        /// Código SKU del artículo
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Descripción del artículo
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Precio de Venta al Detalle
        /// </summary>
        public decimal? PVD { get; set; }

        /// <summary>
        /// Inventario objetivo en piezas
        /// </summary>
        public decimal? InventarioObjetivo { get; set; }

        /// <summary>
        /// Monto objetivo en dinero
        /// </summary>
        public decimal? MontoObjetivo { get; set; }

        /// <summary>
        /// Óptimo anterior en piezas
        /// </summary>
        public int? OptimoAnterior { get; set; }

        /// <summary>
        /// Monto de inventario anterior
        /// </summary>
        public decimal? InvMontoAnterior { get; set; }

        /// <summary>
        /// Óptimo actualizado en piezas
        /// </summary>
        public int? OptimoActualizado { get; set; }

        /// <summary>
        /// Monto de inventario nuevo
        /// </summary>
        public decimal? InvMontoNuevo { get; set; }

        /// <summary>
        /// Relación entre inventario y ventas de Invenadro
        /// </summary>
        public string RelacionInvenadroVenta { get; set; }

        /// <summary>
        /// Estatus del producto (Activo, Inactivo, etc.)
        /// </summary>
        public string EstatusProducto { get; set; }

        /// <summary>
        /// Porcentaje de diferencia en piezas
        /// </summary>
        public decimal? PorcDifPiezas { get; set; }

        /// <summary>
        /// Porcentaje de diferencia en monto
        /// </summary>
        public decimal? PorcDifMonto { get; set; }
    }
}
